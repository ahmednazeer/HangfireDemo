using System.Net.Sockets;

namespace Hangfire.Api.Services
{
    public interface IServiceManagement
    {
        void SyncData();
        void UpdateDatabase();
        void SendEmail();
    }
}
