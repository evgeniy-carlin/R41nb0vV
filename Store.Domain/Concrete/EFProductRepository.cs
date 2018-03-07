using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Entities;
using Store.Domain.Abstract;

namespace Store.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}
