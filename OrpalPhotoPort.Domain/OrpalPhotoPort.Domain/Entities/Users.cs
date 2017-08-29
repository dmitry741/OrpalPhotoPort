using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OrpalPhotoPort.Domain.Entities
{
    public class User
    {
        /// <summary>
        /// User id, integer value
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User nick-name, string value
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User email, string value
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User login, string value
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password, string value
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role: 0 - usual user, 1 - admin, integer value 
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Registration date
        /// </summary>
        public DateTime RegDateTime { get; set; }

        /// <summary>
        /// Status: 0 - active, 1 - banned
        /// </summary>
        public int ActiveStatus { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class UserCongiguratoion : EntityTypeConfiguration<User>
    {
        public UserCongiguratoion()
        {
            // primary key
            HasKey(x => x.Id);

            // flag of autoincrement
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(x => x.Name)
                .HasMaxLength(250)
                .IsRequired();

            Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Login)
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.Password)
                .HasMaxLength(16)
                .IsRequired();

            Property(x => x.Role)
                .IsRequired();

            Property(x => x.RegDateTime)
                .IsRequired();

            Property(x => x.ActiveStatus)
                .IsRequired();
        }
    }
}
