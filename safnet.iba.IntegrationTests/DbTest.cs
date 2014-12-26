using Microsoft.Practices.EnterpriseLibrary.Data;

namespace safnet.iba.IntegrationTests
{
    public abstract class DbTest
    {
        static DbTest()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }

        protected DbTest()
        {
        }
    }
}
