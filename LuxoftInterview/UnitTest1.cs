using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LuxoftInterview
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver = new ChromeDriver();

        [TestInitialize]
        public void openBrowser()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            Actions mouseAction = new Actions(driver);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //Click Women section
            IWebElement womenTitle = driver.FindElement(By.XPath("//a[@title='Women' and @class='sf-with-ul']"));
            mouseAction.MoveToElement(womenTitle).Perform();

            //Click Summer dresses link
            IWebElement summerDressesSection = driver.FindElement(By.XPath("//*[@id='block_top_menu']/ul/li[1]/ul/li[2]/ul/li[3]/a"));
            summerDressesSection.Click();

            //Wait for page to load
            Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='cat-name']")));
            IWebElement summerDressTitle = driver.FindElement(By.XPath("//span[@class='cat-name']"));
            bool isSummerDressTittleDisplayed = summerDressTitle.Displayed;
            Assert.IsTrue(isSummerDressTittleDisplayed, "Summer Dress page was displayed");
            Thread.Sleep(2000);

            //Select 2nd summer dress
            IWebElement secondSummerDress = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div[2]/ul/li[2]"));
            secondSummerDress.Click();
            Thread.Sleep(2000);

            //Click add to cart button
            IWebElement addToCartButton = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div[2]/ul/li[2]/div/div[2]/div[2]/a[1]/span"));
            addToCartButton.Click();
            Thread.Sleep(2000);

            //Validate item added to cart
            IWebElement dressAddedToCart = driver.FindElement(By.XPath("//h2/preceding::div[@class='layer_cart_product col-xs-12 col-md-6']"));
            bool isTextDisplayed = dressAddedToCart.Text.Contains("Product successfully added");
            if(dressAddedToCart.Text.Contains("Product successfully added") == true)
            { 
                Console.WriteLine("Text confirmation was displayed ");
            }
            Assert.IsTrue(isTextDisplayed, "Text confirmation was not displayed");

            //Close Modal Window
            IWebElement closeModalButton = driver.FindElement(By.XPath("//span[@class='cross' and @title='Close window']"));
            closeModalButton.Click();
            Thread.Sleep(2000);

            //Hover shopping cart
            IWebElement shoppingCartDropdown = driver.FindElement(By.XPath("//b[text()='Cart']"));
            Actions cartFocus = new Actions(driver);
            cartFocus.MoveToElement(shoppingCartDropdown).Perform();
            Thread.Sleep(2000);

            //Click remove dress from cart
            IWebElement removeDressButton = driver.FindElement(By.XPath("//a[@rel='nofollow']/preceding::span[@class='remove_link']"));
            removeDressButton.Click();
            Thread.Sleep(4000);

            //Assert dress was deleted
            IWebElement emptyCartText = driver.FindElement(By.XPath("//span[text()='(empty)']"));
            bool isEmptyCartTextDisplayed = emptyCartText.Displayed;
            Assert.IsTrue(isEmptyCartTextDisplayed, "Dress was not removed from cart");

        }

        [TestCleanup]
        public void closeBrowser()
        {
            driver.Quit();
        }
    }
}
