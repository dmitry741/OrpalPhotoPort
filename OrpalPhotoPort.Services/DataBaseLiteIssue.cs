using System;
using System.Collections.Generic;
using System.Linq;
using OrpalPhotoPort.Domain.Entities;

namespace OrpalPhotoPort.Services
{
    public class DataBaseLiteIssue : Base.IDataBaseEngine
    {
        static IEnumerable<User> s_users;

        private IEnumerable<User> users
        {
            get
            {
                if (s_users == null)
                {
                    int index = 0;

                    User user1 = new User
                    {
                        id = ++index,
                        Name = "Паладин Света",
                        Email = "sergey.suloev@gmail.com",
                        Login = "Paladin",
                        Password = "12345678",
                        Role = 1,
                        RegDateTime = DateTime.Now,
                        ActiveStatus = 0
                    };

                    User user2 = new User
                    {
                        id = ++index,
                        Name = "Орк Правдарез",
                        Email = "dmitrypavlov74@gmail.com",
                        Login = "Orc",
                        Password = "12345678",
                        Role = 1,
                        RegDateTime = DateTime.Now,
                        ActiveStatus = 0
                    };

                    User user3 = new User
                    {
                        id = ++index,
                        Name = "User1",
                        Email = "user1@gmail.com",
                        Login = "user1",
                        Password = "qwerty",
                        Role = 0,
                        RegDateTime = DateTime.Now,
                        ActiveStatus = 0
                    };

                    s_users = new List<User>(new List<User>() { user1, user2, user3 });
                }

                return s_users;
            }
            set
            {
                s_users = value;
            }
        }
       
        public bool AddUser(User model)
        {
            int id = users.Max(x => x.id) + 1;
            model.id = id;
            users = users.Concat(new List<User> { model });
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
            return users.Where(x => x.ActiveStatus == 0);
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public bool RemoveUserAt(int id)
        {
            bool result = false;
            User u = users.FirstOrDefault(x => x.id == id);

            if (u != null)
            {
                var query = users.Except(new List<User>() { u });
                users = query;
                result = true;
            }

            return result;
        }
    }
}
