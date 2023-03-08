using System.Data.Entity;
using HashingDomain.Model;

namespace BlazorGuiServer.Data.Repository.Model
{
    public class SecurePasswordDbContext : DbContext
    {
        public SecurePasswordDbContext() : base("Server=LAPTOP-DRMV9MFV;Database=SecurePasswordHashing;Trusted_Connection=True;")
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}
