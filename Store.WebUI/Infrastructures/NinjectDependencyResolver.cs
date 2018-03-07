using System;
using System.Linq;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Store.Domain.Abstract;
using Store.Domain.Concrete;
using Store.Domain.Entities;

namespace Store.WebUI.Infrastructures
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}