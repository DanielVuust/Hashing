using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashingDomain.Model;

namespace HashingDomain
{
    public class SecurePasswordDbContext : DbContext
    {
        public SecurePasswordDbContext() : base("Server=DESKTOP-RQGFB46;Database=SecurePasswordHashing;Trusted_Connection=True;")
        {
            
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
