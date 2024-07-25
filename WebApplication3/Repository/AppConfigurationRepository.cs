using WebApplication3.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Repository
{
    public class AppConfigurationRepository:IAppConfigurationRepository
    {
        private readonly ProDbContext _context;

        public AppConfigurationRepository(ProDbContext context)
        {
            _context = context;
        }
        public async Task<AppConfiguration> GetAppConfigurationAsync()
        {
            return await _context.AppConfigurations.FirstOrDefaultAsync(); // Adjust this based on your schema
        }
    }
}
