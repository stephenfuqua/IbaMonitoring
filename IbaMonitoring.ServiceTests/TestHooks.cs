using System.Collections.Generic;
using System.Reflection;
using IbaMonitoring.ServiceTests.Orm;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity;
using NServiceKit.OrmLite;
using TechTalk.SpecFlow;

namespace IbaMonitoring.ServiceTests
{
    [Binding]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TestHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        public static IDbConnectionFactory DbFactory { get; set; }
        public static IUnityContainer Ioc { get; set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var connectionString = ConfigureWebApplication();

            ConfigureOrmLite(connectionString);

            ConfigureUnity();
        }

        private static string ConfigureWebApplication()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            return DatabaseFactory.CreateDatabase().ConnectionString;
        }

        private static void ConfigureOrmLite(string connectionString)
        {
            DbFactory = new OrmLiteConnectionFactory(connectionString, 
                SqlServerDialect.Provider);
        }

        private static void ConfigureUnity()
        {
            Ioc = new UnityContainer();

            var assemblyList = new List<Assembly>()
            {
                Assembly.GetAssembly(typeof (safnet.iba.IdentityMap))
            };

            Ioc.RegisterTypes(
                AllClasses.FromAssemblies(assemblyList),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled
                );
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            // Empty out all of the database tables, taking foreign keys 
            // into account through the order of operations
            using (var db = DbFactory.OpenDbConnection())
            {
                db.DeleteAll<Observation>();
                db.DeleteAll<PointSurvey>();
                db.DeleteAll<SiteCondition>();
                db.DeleteAll<SiteVisit>();
                db.DeleteAll<SiteBoundary>();
                db.DeleteAll<Location>();
                db.DeleteAll<Season>();
                db.DeleteAll<Species>();
                db.DeleteAll<Person>();
                db.DeleteAll<IbaProgram>();
                db.DeleteAll<Lookup>();
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

    }
}
