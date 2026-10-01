using Microsoft.EntityFrameworkCore;

namespace API.BuisnessLogics
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
    }
}
