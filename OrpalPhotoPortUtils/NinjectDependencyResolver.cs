using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace OrpalPhotoPortUtils
{
    public class NinjectDependencyResolver
    {
        IKernel m_kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            m_kernel = kernel;
            m_kernel.Bind<Base.ICryptograph>().To<CryptographSym>();
        }

        public IKernel Kernel => m_kernel;
    }
}
