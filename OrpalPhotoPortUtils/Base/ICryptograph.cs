using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrpalPhotoPortUtils.Base
{
    public interface ICryptograph
    {
        string Encode(string message);
        string Decode(string message);
    }
}
