using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using XX.Data.Models.Mapping;

namespace XX.Data.Models
{
    public partial class XXDBContext : DbContext
    {
        static XXDBContext()
        {
            Database.SetInitializer<XXDBContext>(null);
        }

        public XXDBContext()
            : base("Name=XXDBContext")
        {
        }

		        public XXDBContext(string connStringName)
            : base(connStringName)
        {
		//Custom by mhd
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
            this.Database.CreateIfNotExists();
        }

        public DbSet<testuser> testusers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new testuserMap());
        }
    }
}
