﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Model;
using WebApplication1.Interfaces.Services;


namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            var configurationStub = Mock.Of<IConfiguration>();
            var productDataMock = new Mock<IProductData>();
            productDataMock.Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(Enumerable.Empty<Product>());
            var controller = new HomeController(configurationStub);

            var result = controller.Index(productDataMock.Object);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var configurationStub = Mock.Of<IConfiguration>();
            var controller = new HomeController(configurationStub);

            var result = controller.Blog();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Second_Returns_View_String()
        {
            const string expectedString = "Second";
            var configurationStub = Mock.Of<IConfiguration>();
            var controller = new HomeController(configurationStub);

            var result = controller.Second();

            Assert.IsInstanceOfType(result, typeof(ContentResult));
            var contentResult = ((ContentResult)result).Content;
            Assert.AreEqual(expectedString, contentResult);
        }
    }
}
