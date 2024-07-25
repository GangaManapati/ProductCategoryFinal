using WebApplication3.Models;

namespace WebApplication3.Repository
{
    public interface IAppConfigurationRepository
    {
        Task<AppConfiguration> GetAppConfigurationAsync();
    }
}
