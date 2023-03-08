using System.Data.Entity;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Repository.Model
{
    public class SecurePasswordDbContext : DbContext
    {
        public SecurePasswordDbContext() : base("Server=DESKTOP-RQGFB46;Database=SecurePasswordHashing;Trusted_Connection=True;")
        {
            Database.SetInitializer<SecurePasswordDbContext>(new DropCreateDatabaseIfModelChanges<SecurePasswordDbContext>());
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
