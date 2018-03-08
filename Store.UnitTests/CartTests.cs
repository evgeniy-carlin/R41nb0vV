using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;

namespace Store.UnitTests
{
    [TestClass]
    class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //A1
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //A2
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);
            Cart.CartLine[] results = target.Lines.ToArray();

            //A3
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }
    }
}
