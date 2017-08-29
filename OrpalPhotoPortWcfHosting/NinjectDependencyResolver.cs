using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using OrpalPhotoPort.Services.Base;
using OrpalPhotoPort.Services;

namespace OrpalPhotoPortWcfHosting
{
    public class NinjectDependencyResolver
    {
        IKernel m_kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            m_kernel = kernel;
            m_kernel.Bind<IDataBaseEngine>().To<DataBaseEngine>();
        }

        public IKernel Kernel => m_kernel;
    }
}