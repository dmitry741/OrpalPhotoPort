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
                    OrpalPhotoPortUtils.Base.ICryptograph cryptograph = OrpalPhotoPortUtils.CryptographCoClass.GetCryptograph();

                    User user1 = new User
                    {
                        Id = ++index,
                        Name = "Паладин Света",
                        Email = "sergey.suloev@gmail.com",
                        Login = "Paladin",
                        Password = cryptograph.Encode("12345678"),
                        Role = 1,
                        RegDateTime = DateTime.Now,
                        ActiveStatus = 0
                    };

                    User user2 = new User
                    {
                        Id = ++index,
                        Name = "Орк Правдарез",
                        Email = "dmitrypavlov74@gmail.com",
                        Login = "Orc",
                        Password = cryptograph.Encode("12345678"),
                        Role = 1,
                        RegDateTime = DateTime.Now,
                        ActiveStatus = 0
                    };

                    User user3 = new User
                    {
                        Id = ++index,
                        Name = "User1",
                        Email = "user1@gmail.com",
                        Login = "user1",
                        Password = cryptograph.Encode("qwerty"),
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
            model.Id = users.Max(x => x.Id) + 1;
            users = users.Concat(new List<User> { model });
            return true;
        }

        public bool EditUser(User model)
        {
            bool result = false;
            
            if (RemoveUserAt(model.Id))
            {                
                result = AddUser(model);
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
            User u = users.FirstOrDefault(x => x.Id == id);

            if (u != null)
            {
                users = users.Except(new List<User>() { u });
                result = true;
            }

            return result;
        }
    }
}
