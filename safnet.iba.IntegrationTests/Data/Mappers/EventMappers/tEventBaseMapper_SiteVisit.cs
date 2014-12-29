using Microsoft.VisualStudio.TestTools.UnitTesting;
using safnet.iba.Business.Entities;
using safnet.iba.Data.Mappers;
using safnet.iba.UnitTests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace safnet.iba.IntegrationTests.Data.Mappers.EventMappers
{
    /// <summary>
    /// Tests for the <see cref="EventBaseMapper"/> static class.
    /// </summary>
    [TestClass]
    public class tEventBaseMapper_SiteVisit : DbTest
    {
        /// <summary>
        /// Validates than appropriate exception is thrown for a <see cref="SiteVisit"/> with null EndTimeStamp.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void t_CreateSaveParameters_SiteVisit_NullEndTimeStamp()
        {
            SiteVisit survey = new SiteVisit()
            {
                EndTimeStamp = null,
                Id = TestHelper.TestGuid1,
                LocationId = TestHelper.TestGuid2,
                ObserverId = TestHelper.TestGuid4,
                RecorderId = TestHelper.TestGuid3,
                StartConditions = new SiteCondition(),
                StartTimeStamp = DateTime.Now
            };

            EventBaseMapper.CreateSaveParameters(survey);
        }

        /// <summary>
        /// Validates that query parameters are properly created for an <see cref="Event"/> object of type <see cref="SiteVisit"/>.
        /// </summary>
        [TestMethod]
        public void t_CreateSaveParameters_SiteVisit_Normal()
        {
            SiteVisit survey = new SiteVisit()
            {
                EndTimeStamp = DateTime.Now,
                Id = TestHelper.TestGuid1,
                LocationId = TestHelper.TestGuid2,
                ObserverId = TestHelper.TestGuid4,
                RecorderId = TestHelper.TestGuid3,
                StartConditions = new SiteCondition(),
                StartTimeStamp = DateTime.Now.AddHours(-1.0)
            };

            List<QueryParameter> resultList = EventBaseMapper.CreateSaveParameters(survey);
            
            Assert.IsNotNull(resultList, "Result list is null");
            Assert.AreEqual(4, resultList.Count, "Wrong number of parameters");
            Assert.AreEqual(survey.Id, resultList.First(x => x.Name.Equals("Id")).Value, "Id");
            Assert.AreEqual(survey.EndTimeStamp, resultList.First(x => x.Name.Equals("EndTime")).Value, "EndTimeStamp");
            Assert.AreEqual(survey.LocationId, resultList.First(x => x.Name.Equals("LocationId")).Value, "LocationId");
            Assert.AreEqual(survey.StartTimeStamp, resultList.First(x => x.Name.Equals("StartTime")).Value, "StartTimeStamp");
        }

        /// <summary>
        /// Validates that the StartTimeStamp, EndTimeStamp, Id, and LocationId are properly filled into 
        /// an <see cref="Event"/> object of type <see cref="SiteVisit"/>.
        /// </summary>
        [TestMethod]
        public void t_Load_SiteVisit_Normal()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("EndTime", typeof(DateTime));
            table.Columns.Add("LocationId", typeof(Guid));
            table.Columns.Add("StartTime", typeof(DateTime));
            table.Columns.Add("ObserverId", typeof(Guid));
            table.Columns.Add("RecorderId", typeof(Guid));

            Guid id = TestHelper.TestGuid1;
            Guid locationId = TestHelper.TestGuid2;
            DateTime? start = DateTime.Now;
            DateTime? end = DateTime.Now.AddHours(1);
            Guid observer = TestHelper.TestGuid3;
            Guid recorder = TestHelper.TestGuid4;

            DataRow row = table.NewRow();
            row["Id"] = id;
            row["EndTime"] = end;
            row["LocationId"] = locationId;
            row["StartTime"] = start;
            row["ObserverId"] = observer;
            row["RecorderId"] = recorder;

            table.Rows.Add(row);

            IDataReader reader = table.CreateDataReader();
            Assert.IsTrue(reader.Read(), "Reader failed");

            SiteVisit survey = new SiteVisit();
            EventBaseMapper.Load(reader, survey);

            Assert.IsNotNull(survey, "Survey object is null");
            Assert.AreEqual(id, survey.Id, "Id");
            Assert.AreEqual(locationId, survey.LocationId, "LocationId");
            Assert.AreEqual(start, survey.StartTimeStamp, "StartTimeStamp");
            Assert.AreEqual(end, survey.EndTimeStamp, "EndTimeStamp");
        }
    }
}
