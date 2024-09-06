using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.Abstractions.Services.EFxceptions;
using STX.EFxceptions.SqlServer;
using WebApplication1.Models;

namespace WebApplication1.Brokers

{
    public interface IStorageBroker
    {
        
    }
    public class StorageBroker : EFxceptionsContext, IStorageBroker

    {
        private IConfiguration configuration;
        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();

        }
        public DbSet<Subject> subjects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionResult = this.configuration.GetConnectionString("defoultConnection");
            optionsBuilder.UseSqlServer(connectionResult);
        }

    }
}
