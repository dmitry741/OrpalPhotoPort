﻿using System;
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
using Ninject;

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
            IKernel standartKernel = new StandardKernel();
            NinjectDependencyResolver ndr = new NinjectDependencyResolver(standartKernel);
            IKernel ninjectKernel = ndr.Kernel;

            m_idbe = ninjectKernel.Get<IDataBaseEngine>();

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
            var list = m_idbe.GetActiveUsers();
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

        public bool RemoveUserAt(int id)
        {
            return m_idbe.RemoveUserAt(id);
        }
    }
}
