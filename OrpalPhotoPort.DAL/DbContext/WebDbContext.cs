using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OrpalPhotoPort.Domain.Entities;

namespace OrpalPhotoPort.DAL.DbContext
{
    public class WebDbContext : System.Data.Entity.DbContext
    {
        public WebDbContext() : base("Data Source=localhost;Integrated Security=False;User ID=u0140060_dbhost;Connect Timeout=15;Encrypt=False;Packet Size=4096") { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserCongiguratoion());
        }
    }
}
