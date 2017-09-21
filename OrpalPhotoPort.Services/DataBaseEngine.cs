using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrpalPhotoPort.Domain.Entities;
using System.Data.SqlClient;
using System.Data;

namespace OrpalPhotoPort.Services
{
    public class DataBaseEngine : Base.IDataBaseEngine
    {
        readonly DAL.DbContext.WebDbContext m_dbContect = new DAL.DbContext.WebDbContext();

        public bool AddUser(User model)
        {
            bool result = false;
            User user = m_dbContect.Users.FirstOrDefault(x => x.Email.ToUpper() == model.Email.ToUpper() || x.Login.ToUpper() == model.Login.ToUpper());

            if (user == null)
            {
                m_dbContect.Users.Add(model);
                m_dbContect.SaveChanges();
                result = true;
            }

            return result;
        }

        public bool EditUser(User model)
        {
            bool result = false;
            User user = m_dbContect.Users.FirstOrDefault(x => x.Id == model.Id);

            if (user != null)
            {
                m_dbContect.Users.Remove(user);
                m_dbContect.Users.Add(model);
                m_dbContect.SaveChanges();
                result = true;
            }

            return result;
        }

        public IEnumerable<User> GetUsers()
        {
            //return m_dbContect.Users;

            DataTable table = new DataTable("assortment");
            
            using (var sql = new SqlConnection("Data Source=localhost;Integrated Security=False;User ID=u0140060_dbhost;Connect Timeout=15;Encrypt=False;Packet Size=4096"))
            {
                var sel = new SqlCommand("SELECT * FROM USERS", sql);
                var adapter = new SqlDataAdapter(sel);                
                adapter.Fill(table);
            }

            List<User> collection = new List<User>();

            foreach (DataRow row in table.Rows)
            {
                User user = new User
                {
                    Name = (string)row["Name"],
                    Email = (string)row["Email"],
                    Login = (string)row["Login"],
                    Password = (string)row["Password"],
                    Role = (int)row["Role"],
                    RegDateTime = DateTime.Now,
                    ActiveStatus = (int)row["ActiveStatus"]
                };

                collection.Add(user);
            }

            return collection;
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return m_dbContect.Users.Where(x => x.ActiveStatus == 0);
        }

        public bool RemoveUserAt(int id)
        {
            bool result = false;
            User user = m_dbContect.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                m_dbContect.Users.Remove(user);
                m_dbContect.SaveChanges();
                result = true;
            }

            return result;
        }
    }
}
