using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrpalPhotoPort.Domain.Entities;

namespace OrpalPhotoPort.Services
{
    public class DataBaseEngine : Base.IDataBaseEngine
    {
        readonly DAL.DbContext.WebDbContext m_dbContect = new DAL.DbContext.WebDbContext();

        public bool AddUser(User model)
        {
            bool result = false;
            User user = m_dbContect.Users.FirstOrDefault(x => x.Email == model.Email || x.Login == model.Login);

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
            User user = m_dbContect.Users.FirstOrDefault(x => x.id == model.id);

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
            return m_dbContect.Users;
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return m_dbContect.Users.Where(x => x.ActiveStatus == 0);
        }

        public bool RemoveUserAt(int id)
        {
            bool result = false;
            User user = m_dbContect.Users.FirstOrDefault(x => x.id == id);

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
