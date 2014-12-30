using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using TechTalk.SpecFlow;
using IbaMonitoring.ServiceTests.Orm;
using IbaMonitoring.UnitTests.Presenters;
using IbaMonitoring.Views;
using NServiceKit.OrmLite;
using TechTalk.SpecFlow.Assist;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IbaMonitoring.ServiceTests
{
    [ExcludeFromCodeCoverage]
    public class SiteConditionForm : ISiteConditionsView
    {
        public string Observer { get; set; }
        public string Recorder { get; set; }
        public string SiteVisited { get; set; }
        public string StartSky { get; set; }
        public string StartUnit { get; set; }
        public string StartTemp { get; set; }
        public string StartWind { get; set; }
        public string StartTime { get; set; }
        public string EndSky { get; set; }
        public string EndUnit { get; set; }
        public string EndTemp { get; set; }
        public string EndWind { get; set; }
        public string EndTime { get; set; }
        public string VisitDate { get; set; }
        public bool IsValid { get; set; }
    }


    [Binding]
    [ExcludeFromCodeCoverage]
    public class SiteConditionsSteps
    {
        private static Dictionary<string, Guid> Locations { get; set; }
        private static Dictionary<string, byte> Wind { get; set; }
        private static Dictionary<string, byte> Sky { get; set; }
        private static Dictionary<string, Guid> People { get; set; }

        public SiteConditionsSteps()
        {
            Locations = new Dictionary<string, Guid>();
            Wind = new Dictionary<string, byte>();
            Sky = new Dictionary<string, byte>();
            People = new Dictionary<string, Guid>();
        }

        private void OrmCall(Action<IDbConnection> action)
        {
            using (var db = TestHooks.DbFactory.OpenDbConnection())
            {
                action(db);
            }
        }

        [Given(@"I can choose from these sites:")]
        public void GivenICanChooseFromTheseSites(Table table)
        {
            OrmCall((IDbConnection db) =>
            {
                var locationTypeId = Guid.NewGuid();
                db.Save(new Lookup { Id = locationTypeId, Value = "" });

                foreach (var row in table.Rows)
                {
                    var location = new Location
                    {
                        LocationName = row["Site Name"],
                        Id = Guid.NewGuid(),
                        LocationTypeId = locationTypeId
                    };
                    db.Save(location);
                    Locations.Add(location.LocationName.Trim(), location.Id);
                }
            });
        }

        [Given(@"I can choose from these observers and recorders:")]
        public void GivenICanChooseFromTheseObserversAndRecorders(Table table)
        {
            OrmCall((IDbConnection db) =>
            {
                foreach (var row in table.Rows)
                {
                    var person = new Person
                    {
                        Id = Guid.NewGuid(),
                        FirstName = row["First"],
                        LastName = row["Last"]
                    };
                    db.Save(person);
                    People.Add(person.FirstName.Trim() + " " + person.LastName.Trim(), person.Id);
                }
            });
        }

        [Given(@"I can choose from these sky conditions:")]
        public void GivenICanChooseFromTheseSkyConditions(Table table)
        {
            foreach (var row in table.Rows)
            {
                Sky.Add(row[1], byte.Parse(row[0]));
            }
        }

        [Given(@"I can choose from these wind conditions:")]
        public void GivenICanChooseFromTheseWindConditions(Table table)
        {
            foreach (var row in table.Rows)
            {
                Wind.Add(row[1], byte.Parse(row[0]));
            }
        }

        private static SiteConditionForm _viewModel;

        [Given(@"I have entered these values into the form:")]
        public void GivenIHaveEnteredTheseValuesIntoTheForm(Table table)
        {
            _viewModel = table.CreateInstance<SiteConditionForm>();

            // lookup the correct Id values from the dictionaries, and
            // replace the text that was entered in the feature file
            _viewModel.Observer = People[_viewModel.Observer.Trim()].ToString();
            _viewModel.Recorder = People[_viewModel.Recorder.Trim()].ToString();
            _viewModel.SiteVisited = Locations[_viewModel.SiteVisited.Trim()].ToString();
            _viewModel.EndWind = Wind[_viewModel.EndWind.Trim()].ToString();
            _viewModel.EndSky = Sky[_viewModel.EndSky.Trim()].ToString();
            _viewModel.StartWind = Wind[_viewModel.StartWind.Trim()].ToString();
            _viewModel.StartSky = Sky[_viewModel.StartSky.Trim()].ToString();
        }

        private static UserStateStub _userState;
        private static ResponseStub _httpResponse;

        [When(@"I press Next")]
        public void WhenIPressNext()
        {
            _userState = new UserStateStub();
            _httpResponse = new ResponseStub();

            var presenter = TestHooks.Ioc
                                     .Resolve<SiteConditionsPresenterTss>()
                                     .SetUserState(_userState)
                                     .SetHttpResponse(_httpResponse);

            presenter.SaveConditions(_viewModel);
        }

        [Then(@"I will be directed to the '(.*)' page")]
        public void ThenIWillBeDirectedToThePage(string expectedPage)
        {
            Assert.AreEqual(_httpResponse.RedirectedTo, expectedPage + ".aspx");
        }

        [Then(@"the form data will be saved into the database")]
        public void ThenTheFormDataWillBeSavedIntoTheDatabase()
        {
            OrmCall((IDbConnection db) =>
            {
                var siteVisits = db.Select<SiteVisit>();
                Assert.AreEqual(1, siteVisits.Count(), "SiteVisit count");
                var visit = siteVisits.First();

                var results = db.Select<SiteCondition>();
                Assert.AreEqual(2, results.Count(), "Site Condition result count");



                var expectedTemp = int.Parse(_viewModel.StartTemp);
                var expectedScale = _viewModel.StartUnit;
                var expectedWind = byte.Parse(_viewModel.StartWind);
                var expectedSky = byte.Parse(_viewModel.StartSky);

                ValidateCondition(results, visit.StartConditionId, expectedTemp, expectedScale, expectedWind, expectedSky, visit, "start");

                expectedTemp = int.Parse(_viewModel.EndTemp);
                expectedScale = _viewModel.EndUnit;
                expectedWind = byte.Parse(_viewModel.EndWind);
                expectedSky  = byte.Parse(_viewModel.EndSky);

                ValidateCondition(results, visit.EndConditionId, expectedTemp, expectedScale, expectedWind, expectedSky, visit, "end");
            });
        }

        private static void ValidateCondition(IEnumerable<SiteCondition> results, Guid? expectedId, int expectedTemp, string expectedScale,
            byte expectedWind, byte expectedSky, SiteVisit visit, string condition)
        {
            var actual = results.FirstOrDefault(x => x.Id == expectedId);
            Assert.IsNotNull(actual, condition + " condition not saved in SiteVisit");

            Assert.AreEqual(expectedTemp, actual.Temperature, condition + " Temperature");
            Assert.AreEqual(expectedScale, actual.Scale, condition + " Scale");
            Assert.AreEqual(expectedWind, actual.Wind, condition + " Wind");
            Assert.AreEqual(expectedSky, actual.Sky, condition + " Sky");
            Assert.AreEqual(visit.Id, actual.SiteVisitId, condition + " SiteVisitId");
        }
    }
}
