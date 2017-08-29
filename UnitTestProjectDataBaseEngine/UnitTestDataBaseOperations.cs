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
        public void AddRemoveOparations()
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

            Assert.IsTrue(idbe.AddUser(user));

            User addedUser = idbe.GetUsers().FirstOrDefault(x => x.Login == login);

            Assert.IsNotNull(addedUser);

            Assert.IsTrue(idbe.RemoveUserAt(addedUser.id));

            addedUser = idbe.GetUsers().FirstOrDefault(x => x.Login == login);

            Assert.IsNull(addedUser);
        }
    }
}
