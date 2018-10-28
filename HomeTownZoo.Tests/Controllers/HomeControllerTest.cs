using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeTownZoo;
using HomeTownZoo.Controllers;

namespace HomeTownZoo.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest // testing home controller
    {
        [TestMethod] // #1 tests home controllers index action
        public void Index_ReturnsNonNullViewResults()
        {
            // ARRANGE
            HomeController controller = new HomeController();

            // ACT by calling the method we want to test
            ViewResult result = controller.Index() as ViewResult;   // call controller Index action result method
                                                                    // returns data type ViewResult with result as new variable
            // ASSERT 
            Assert.IsNotNull(result);
 
        }

        [TestMethod] // #2 tests home controllers about action
        public void About_ReturnsNonNullViewResult()
        {
            // ARRANGE
            HomeController home = new HomeController();

            // ACT by calling the method we want to test
            ViewResult result = home.About() as ViewResult; // call controller Index action result method
                                                                  // returns data type ViewResult with result as new variable
            // ASSERT 
            Assert.IsNotNull(result);
        }


        [TestMethod] // #3
        public void About_ShouldHaveViewBagMessage()
        {
            // ACT
            HomeController home = new HomeController();

            // ACT by calling the method we want to test
            ViewResult result = home.About() as ViewResult;

            // ASSERT
            Assert.IsNotNull(result.ViewBag.Message);
           
            // OR check that viewbag string matches specific string message
            Assert.AreEqual("About Hometown Zoo", result.ViewBag.Message);
            
            // OR 
            Assert.AreNotEqual(string.Empty, ((string)result.ViewBag.Message).Trim());
            
            // OR ASSERT USING A CREATED VARIABLE
            string message = result.ViewBag.Message;
            Assert.IsNotNull(message);
            Assert.AreNotEqual(string.Empty, message.Trim());
        }



    }
}