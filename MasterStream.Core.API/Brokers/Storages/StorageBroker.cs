//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.SqlServer;

namespace MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages
{
    internal partial class StorageBroker : EFxceptionsContext
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
