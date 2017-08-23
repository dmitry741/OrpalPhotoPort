using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OrpalPhotoPort.Domain.DataContractMemebers;
using OrpalPhotoPort.Domain.Entities;
using OrpalPhotoPort.Services.Base;
using OrpalPhotoPort.Services;
using AutoMapper;

namespace OrpalPhotoPortWcfHosting
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WebDbServive : IWebDbService
    {
        IDataBaseEngine m_idbe;
        IMapper m_mapper1;
        IMapper m_mapper2;

        public WebDbServive()
        {
            m_idbe = new DataBaseEngine();

            // mapping User -> UserDataContract
            MapperConfiguration mapConfig1 = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDataContract>());
            m_mapper1 = mapConfig1.CreateMapper();

            // mapping UserDataContract -> User
            MapperConfiguration mapConfig2 = new MapperConfiguration(cfg => cfg.CreateMap<UserDataContract, User>());
            m_mapper2 = mapConfig2.CreateMapper();
        }

        public IEnumerable<UserDataContract> GetUsers()
        {
            var list = m_idbe.GetUsers(); // all
            return m_mapper1.Map<IEnumerable<User>, List<UserDataContract>>(list);
        }

        public IEnumerable<UserDataContract> GetActiveUsers()
        {
            var list = m_idbe.GetUsers().Where(x => x.ActiveStatus == 0); // only active
            return m_mapper1.Map<IEnumerable<User>, List<UserDataContract>>(list);
        }

        public bool AddUser(UserDataContract udc)
        {
            return m_idbe.AddUser(m_mapper2.Map<User>(udc));
        }

        public bool EditUser(UserDataContract udc)
        {
            return m_idbe.EditUser(m_mapper2.Map<User>(udc));
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
