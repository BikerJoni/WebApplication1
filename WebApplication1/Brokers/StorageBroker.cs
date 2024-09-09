using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.Abstractions.Services.EFxceptions;
using STX.EFxceptions.SqlServer;
using WebApplication1.Models;

namespace WebApplication1.Brokers

{
    public interface IStorageBroker
    {
        Task<Subject> InsertSubjectAsync(Subject subject);
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



        public async Task<Subject> InsertSubjectAsync(Subject subject)
        {
            StorageBroker storageBroker = new StorageBroker(this.configuration);
            await storageBroker.subjects.AddAsync(subject);
            await storageBroker.SaveChangesAsync();

            return subject;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionResult = this.configuration.GetConnectionString("defoultConnection");
            optionsBuilder.UseSqlServer(connectionResult);
        }

    }
}
