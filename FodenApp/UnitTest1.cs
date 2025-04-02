using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace FodenApp;

public class Tests
{
    IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        Console.WriteLine("SetupConsole");
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        // Initialize the ChromeDriver
        driver = new ChromeDriver();
    }

    [Test]
    public void Test1()
    {

        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://rahulshettyacademy.com/");
        TestContext.Progress.WriteLine(driver.Title);
        Console.WriteLine(driver.Title);
        Assert.Pass();
    }

    [Test]
    public void Test2()
    {
        Assert.Pass();
    }

    [TearDown]
    public void TearDown()
    {
        Console.WriteLine("TearDownConsole");
        driver.Quit();
        driver.Dispose();
    }
}
