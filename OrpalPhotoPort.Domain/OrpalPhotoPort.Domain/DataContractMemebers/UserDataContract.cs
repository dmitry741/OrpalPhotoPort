using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OrpalPhotoPort.Domain.DataContractMemebers
{
    [DataContract]
    public class UserDataContract
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int Role { get; set; }

        [DataMember]
        public DateTime RegDateTime { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
