using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using OrpalPhotoPort.Services.Base;
using OrpalPhotoPort.Services;
using OrpalPhotoPort.Domain.Entities;
using Ninject;

namespace UnitTestProjectDataBaseEngine
{
    [TestClass]
    public class UnitTestDataBaseOperations
    {
        IDataBaseEngine GetDataBaseCoClass()
        {
            IKernel standartKernel = new StandardKernel();
            standartKernel.Bind<IDataBaseEngine>().To<DataBaseLiteIssue>();

            return standartKernel.Get<IDataBaseEngine>();
        }

        [TestMethod]
        public void AddRemoveOperations()
        {
            IDataBaseEngine idbe = GetDataBaseCoClass();

            int curCount = idbe.GetUsers().Count();
            DateTime now = DateTime.Now;
            string login = "user" + now.Second + now.Minute + now.Hour + now.Day + now.Month + now.Year;

            User user = new User
            {
                Name = "Random user",
                Email = Guid.NewGuid().ToString().Replace("-", string.Empty) + "@gmail.ru",
                Login = login,
                Password = "12345678",
                Role = 0,
                RegDateTime = now,
                ActiveStatus = 0
            };

            // add new user
            Assert.IsTrue(idbe.AddUser(user));

            // get user by login
            User addedUser = idbe.GetUsers().FirstOrDefault(x => x.Login == login);

            // user is not null
            Assert.IsNotNull(addedUser);

            // remove user by id
            Assert.IsTrue(idbe.RemoveUserAt(addedUser.Id));

            // get user by login
            addedUser = idbe.GetUsers().FirstOrDefault(x => x.Login == login);

            // user has to be equal to null
            Assert.IsNull(addedUser);
        }
    }
}
