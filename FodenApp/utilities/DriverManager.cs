using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace FodenApp.utilities
{
    public class DriverManager
    {

        // private static IWebDriver driver;
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        String browserName;
        public static IWebDriver getDriver()
        {
            return driver.Value;
        }

        public void InitBrowser(String browser)
        {
            switch (browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
                default:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
            }
        }

        [SetUp]
        public void StartBrowser()
        {
            TestContext.Progress.WriteLine("SetupTestContext");
            // String browser = ConfigurationManager.AppSettings.Get("browser");
            browserName = TestContext.Parameters["browserName"]; //To get the value passed from command line (run time)
            if (browserName == null) //If no value is passed during entering the command to run, get the default value from App.config file
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            InitBrowser(browserName);
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
            // wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

        [TearDown]
        public void TearDownBrowser()
        {
            Console.WriteLine("TearDownConsole");
            driver.Value.Quit();
        }

        [OneTimeTearDown]
        public void DisposeDriver()
        {
            if (driver.IsValueCreated)
            {
                driver.Value.Dispose();
            }
            driver.Dispose();
        }

    }
}