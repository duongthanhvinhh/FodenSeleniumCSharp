using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework.Legacy;

namespace FodenApp;

public class Locators
{
    IWebDriver driver;
    WebDriverWait wait;

    [SetUp]
    public void StartBrowser()
    {
        Console.WriteLine("SetupConsole");
        TestContext.Progress.WriteLine("SetupTestContext");
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
        wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));        
    }

    [Test]
    [Retry(1)]
    [Category("Smoke")]
    public void VerifyThatUserCanLoginSuccessfully()
    {
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='username']")));
        driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("Admin");
        driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin123");
        driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        // Assert.That(driver.FindElement(By.XPath("//h6[text()='Dashboard']")).Displayed);
        // wait.Until(d => d.FindElement(By.XPath("//p[text()='Punched In']")).Displayed);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[text()='Punched In']")));
        IWebElement ele = driver.FindElement(By.XPath("//p[text()='Punched In']"));
        String className = ele.GetAttribute("class");
        Assert.That(className, Is.EqualTo("oxd-text oxd-text--p orangehrm-attendance-card-state"));
        // ClassicAssert.AreEqual(className, "oxd-text oxd-text--p orangehrm-attendance-card-state"); //Not recommended to use anymore
    }


    [TearDown]
    public void TearDownBrowser()
    {
        Console.WriteLine("TearDownConsole");
        driver.Quit();
        driver.Dispose();
    }
}