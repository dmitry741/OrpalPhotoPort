using OrpalPhotoPortUtils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace OrpalPhotoPortUtils
{
    public class CryptographCoClass
    {
        static public ICryptograph GetCryptograph()
        {
            IKernel standartKernel = new StandardKernel();
            NinjectDependencyResolver ndr = new NinjectDependencyResolver(standartKernel);
            IKernel ninjectKernel = ndr.Kernel;

            return ninjectKernel.Get<ICryptograph>();
        }
    }
}
