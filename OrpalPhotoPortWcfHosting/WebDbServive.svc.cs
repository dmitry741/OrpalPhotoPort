using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OrpalPhotoPort.Domain.DataContractMemebers;
using OrpalPhotoPort.Services.Base;
using OrpalPhotoPort.Services;

namespace OrpalPhotoPortWcfHosting
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WebDbServive : IWebDbService
    {
        IDataBaseEngine m_idbe;

        public WebDbServive()
        {
            m_idbe = new DataBaseEngine();
        }

        public IEnumerable<UserDataContract> GetUsers()
        {
            var list = m_idbe.GetUsers();
            List<UserDataContract> result = new List<UserDataContract>();

            foreach (var p in list)
            {
                // mapping entity on UserDataContract
                UserDataContract pdc = new UserDataContract
                {
                    id = p.id,
                    Name = p.Name,
                    Login = p.Login,
                    Email = p.Email,
                    Password = p.Password,
                    Role = p.Role,
                    RegDateTime = p.RegDateTime,
                    ActiveStatus = p.ActiveStatus
                };

                result.Add(pdc);
            }

            return result;
        }

        public IEnumerable<UserDataContract> GetActiveUsers()
        {
            var templist = m_idbe.GetUsers();
            var list = templist.Where(x => x.ActiveStatus == 0);
            List<UserDataContract> result = new List<UserDataContract>();

            foreach (var p in list)
            {
                // mapping entity on UserDataContract
                UserDataContract pdc = new UserDataContract
                {
                    id = p.id,
                    Name = p.Name,
                    Login = p.Login,
                    Email = p.Email,
                    Password = p.Password,
                    Role = p.Role,
                    RegDateTime = p.RegDateTime,
                    ActiveStatus = p.ActiveStatus
                };

                result.Add(pdc);
            }

            return result;
        }

        public bool AddUser(UserDataContract udc)
        {
            //  mapping UserDataContract on entity
            OrpalPhotoPort.Domain.Entities.User u = new OrpalPhotoPort.Domain.Entities.User
            {
                Name = udc.Name,
                Email = udc.Email,
                Login = udc.Login,
                Password = udc.Password,
                RegDateTime = udc.RegDateTime,
                Role = udc.Role,
                ActiveStatus = udc.ActiveStatus,
            };

            return m_idbe.AddUser(u);
        }

        public bool EditUser(UserDataContract udc)
        {
            //  mapping UserDataContract on DB Entity
            OrpalPhotoPort.Domain.Entities.User u = new OrpalPhotoPort.Domain.Entities.User
            {
                id = udc.id,
                Name = udc.Name,
                Email = udc.Email,
                Login = udc.Login,
                Password = udc.Password,
                RegDateTime = udc.RegDateTime,
                Role = udc.Role,
                ActiveStatus = udc.ActiveStatus
            };

            return m_idbe.EditUser(u);
        }

        //public string GetData(int value)
        //{
        //    return string.Format("You entered: {0}", value);
        //}

        //public CompositeType GetDataUsingDataContract(CompositeType composite)
        //{
        //    if (composite == null)
        //    {
        //        throw new ArgumentNullException("composite");
        //    }
        //    if (composite.BoolValue)
        //    {
        //        composite.StringValue += "Suffix";
        //    }
        //    return composite;
        //}
    }
}
