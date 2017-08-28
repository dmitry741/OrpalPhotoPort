using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrpalPhotoPort.Domain.Entities;

namespace OrpalPhotoPort.Services
{
    public class DataBaseLiteIssue : Base.IDataBaseEngine
    {
        IEnumerable<User> m_users;

        public DataBaseLiteIssue()
        {
            User user1 = new User
            {
                Name = "Паладин Света",
                Email = "sergey.suloev@gmail.com",
                Login = "Paladin",
                Password = "12345",
                Role = 1,
                RegDateTime = DateTime.Now,
                ActiveStatus = 0
            };

            User user2 = new User
            {
                Name = "Орк Правдарез",
                Email = "dmitrypavlov74@gmail.com",
                Login = "Orc",
                Password = "12345",
                Role = 1,
                RegDateTime = DateTime.Now,
                ActiveStatus = 0
            };

            m_users = new List<User>(new List<User>() { user1, user2 });
        }
        
        public bool AddUser(User model)
        {
            m_users = m_users.Concat(new List<User> { model });
            return true;
        }

        public bool EditUser(User model)
        {
            bool result = false;
            
            if (RemoveUserAt(model.id))
            {
                AddUser(model);
                result = true;
            }

            return result;
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return m_users.Where(x => x.ActiveStatus == 0);
        }

        public IEnumerable<User> GetUsers()
        {
            return m_users;
        }

        public bool RemoveUserAt(int id)
        {
            bool result = false;
            User u = m_users.FirstOrDefault(x => x.id == id);

            if (u != null)
            {
                var query = m_users.Except(new List<User>() { u });
                m_users = query;
                result = true;
            }

            return result;
        }
    }
}
