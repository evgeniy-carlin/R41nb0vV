﻿using System;
using System.Linq;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Store.Domain.Abstract;
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

        public void AddBindings()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name = "Nike", Price = 1299},
                new Product {Name = "Puma", Price = 699},
                new Product {Name = "Adidas", Price = 900}
            });

            kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}