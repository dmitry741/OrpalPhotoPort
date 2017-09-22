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
using Ninject;
using System.Data.SqlClient;
using System.Data;

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
            //var list = m_idbe.GetUsers(); // all

            var sql = new SqlConnection("Data Source=localhost;Integrated Security=False;User ID=u0140060_dbhost; Password=Brq99q0^; Connect Timeout=15;Encrypt=False;Packet Size=4096");
            var sel = new SqlCommand("select * from users", sql);
            var adapter = new SqlDataAdapter(sel);
            DataTable table = new DataTable("assortment");
            adapter.Fill(table);
            sql.Close();

            List<UserDataContract> listUDC = new List<UserDataContract>();

            foreach (DataRow row in table.Rows)
            {
                UserDataContract udc = new UserDataContract
                {
                    id = (int)row["id"],
                    Name = (string)row["Name"],
                    Email = (string)row["Email"],
                    Login = (string)row["Login"],
                    Password = (string)row["Password"],
                    Role = (int)row["Role"],
                    RegDateTime = (DateTime)row["RegDateTime"],
                    ActiveStatus = (int)row["ActiveStatus"],
                };

                listUDC.Add(udc);
            }

            return listUDC;



            //List<UserDataContract> listUDC = new List<UserDataContract>();

            //foreach (User u in list)
            //{
            //    UserDataContract udc = new UserDataContract
            //    {
            //        id = u.Id,
            //        Name = u.Name,
            //        Email = u.Email,
            //        Login = u.Login,
            //        Password = u.Password,
            //        Role = u.Role,
            //        RegDateTime = u.RegDateTime,
            //        ActiveStatus = u.ActiveStatus
            //    };

            //    listUDC.Add(udc);
            //}

            //return listUDC;

            //return m_mapper1.Map<IEnumerable<User>, List<UserDataContract>>(list);
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
