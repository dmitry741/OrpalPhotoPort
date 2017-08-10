using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace OrpalPhotoPort.Services.Base
{
    [ServiceContract]
    public interface IDataBaseEngine
    {
        [OperationContract]
        IEnumerable<Domain.Entities.User> GetUsers();

        [OperationContract]
        bool RemoveUserAt(int id);

        [OperationContract]
        bool AddUser(Domain.Entities.User model);

        [OperationContract]
        bool EditUser(Domain.Entities.User model);
    }
}
