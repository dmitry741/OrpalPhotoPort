using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using OrpalPhotoPort.Domain.Entities;


namespace OrpalPhotoPort.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DbContext.WebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DbContext.WebDbContext context)
        {
            // remove all
            List<User> all = new List<User>(context.Users);
            context.Users.RemoveRange(all);

            context.Users.AddOrUpdate(e => e.id,
            new User
            {
                Name = "Паладин Света",
                Email = "sergey.suloev@gmail.com",
                Login = "Paladin",
                Password = "12345",
                Role = 1,
                RegDateTime = DateTime.Now,
                IsDeleted = false
            },

            new User
            {
                Name = "Орк Правдарез",
                Email = "dmitrypavlov74@gmail.com",
                Login = "Orc",
                Password = "12345",
                Role = 1,
                RegDateTime = DateTime.Now,
                IsDeleted = false
            }
            );

            context.SaveChanges();
        }
    }
}
