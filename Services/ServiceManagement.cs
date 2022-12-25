namespace Hangfire.Api.Services
{
    public class ServiceManagement : IServiceManagement
    {
        public void SendEmail()
        {
            Console.WriteLine($"Send email at {DateTime.Now}");
        }

        public void SyncData()
        {
            Console.WriteLine($"Data is syncing at {DateTime.Now}");
        }

        public void UpdateDatabase()
        {
            Console.WriteLine($"Database is updating now ");
        }
    }
}
