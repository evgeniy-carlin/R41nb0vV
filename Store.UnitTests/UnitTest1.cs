using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;
using Store.WebUI.HtmlHelpers;
using Store.WebUI.Controllers;

namespace Store.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //A1 - организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //A2 - действие
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            //A3 - утверждение
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //A1
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //A2
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //A3
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" + @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //A1 - организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3,Name = "P3"},
                new Product {ProductID = 4,Name = "P4"},
                new Product {ProductID = 5,Name = "P5"}
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //A2
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;

            //A3
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //A1 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Categoty = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Categoty = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Categoty = "Cat3"},
                new Product {ProductID = 4, Name = "P4", Categoty = "Cat4"},
                new Product {ProductID = 5, Name = "P5", Categoty = "Cat5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //A2
            Product[] result = ((ProductListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //A3
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Categoty == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Categoty == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Categoty = "Apples"},
                new Product {ProductID = 2, Name = "P2", Categoty = "Apples"},
                new Product {ProductID = 3, Name = "P3", Categoty = "Plums"},
                new Product {ProductID = 4, Name = "P4", Categoty = "Oranges"},
            });

            NavController target = new NavController(mock.Object);

            //A2
            string[] result = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //A3
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "Apples");
            Assert.AreEqual(result[1], "Oranges");
            Assert.AreEqual(result[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Categoty = "Apples"},
                new Product {ProductID = 4, Name = "P2", Categoty = "Oranges"},
            });

            NavController target = new NavController(mock.Object);

            string categoryToSelect = "Apples";

            //A2
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //A3
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //A1
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Categoty = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Categoty = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Categoty = "Cat3"},
                new Product {ProductID = 4, Name = "P4", Categoty = "Cat4"},
                new Product {ProductID = 5, Name = "P5", Categoty = "Cat5"},
            });

            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            //A2
            int res1 = ((ProductListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel)target.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductListViewModel)target.List("Cat1").Model).PagingInfo.TotalItems;
            int resAll = ((ProductListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            //A3
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void Can_Add_Quantity_Exiting_Lines()
        {
            //A1
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //A2
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            Cart.CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            //A3
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Quantity, 11);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //A1
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //A2
            target.RemoveLine(p2);

            //A3
            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //A1
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            //A2
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //A3
            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Can_Clean_Contents()
        {
            //A1
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            //A2
            target.Clear();

            //A3
            Assert.AreEqual(target.Lines.Count(), 0);
        }
    }
}
