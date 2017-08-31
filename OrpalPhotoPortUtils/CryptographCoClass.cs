using OrpalPhotoPortUtils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrpalPhotoPortUtils
{
    public class CryptographCoClass
    {
        static public ICryptograph GetCryptograph()
        {
            return new Cryptograph();
        }
    }
}
