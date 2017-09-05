using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OrpalPhotoPort.Domain.DataContractMemebers;


namespace OrpalPhotoPortWcfHosting
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWebDbService
    {
        [OperationContract]
        IEnumerable<UserDataContract> GetUsers();

        [OperationContract]
        IEnumerable<UserDataContract> GetActiveUsers();

        [OperationContract]
        bool AddUser(UserDataContract udc);

        [OperationContract]
        bool EditUser(UserDataContract udc);

        [OperationContract]
        bool RemoveUserAt(int id);
    }
}
