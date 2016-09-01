using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private string baseURL = "http://organizationdetails20160803webapp.azurewebsites.net/";
        private RemoteWebDriver driver;
        private string browser;
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(19)]
        [Owner("FireFox")] //Using Owner as Category trait is not supported by the DTA Task
        public void RemoteSelenium()
        {
            DesiredCapabilities capability = DesiredCapabilities.Firefox();
            Uri server = new Uri("http://localhost:4445/wd/hub");            
            this.driver = new RemoteWebDriver(server, capability);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(this.baseURL);
            driver.FindElementById("search - box").Clear();
            driver.FindElementById("search - box").SendKeys("tire");
            //do other Selenium things here!
        }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(5)]
        [Owner("BuildSet")] //Using Owner as Category trait is not supported by the DTA Task
        public void TireSearch_Any()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(this.baseURL);
            driver.FindElementById("search - box").Clear();
            driver.FindElementById("search - box").SendKeys("tire");
            //do other Selenium things here!
        }

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {   //Set the browswer from a build
            browser = this.TestContext.Properties["browser"] != null ? this.TestContext.Properties["browser"].ToString() : "firefox";
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                case "PhantomJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    driver = new PhantomJSDriver();
                    break;
            }
            if (this.TestContext.Properties["Url"] != null) //Set URL from a build
            {
                this.baseURL = this.TestContext.Properties["Url"].ToString();
            }
            else
            {
                this.baseURL = "http://organizationdetails20160803webapp.azurewebsites.net/";   //default URL just to get started with
            }
        }

    }
}
