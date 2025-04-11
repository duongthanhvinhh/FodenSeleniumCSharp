using FodenApp.enums;
using FodenApp.utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FodenApp.pages
{
    public class Navbar
    {
        private readonly IWebDriver driver;

        public Navbar(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        private By adminOption = By.XPath("//span[text()='Admin']");
        private By PIMOption = By.XPath("//span[text()='PIM']");
        private By leaveOption = By.XPath("//span[text()='Leave']");
        private By timeOption = By.XPath("//span[text()='Time']");
        private By recruitmentOption = By.XPath("//span[text()='Recruitment']");
        private By myInfoOption = By.XPath("//span[text()='My Info']");
        private By performanceOption = By.XPath("//span[text()='Performance']");
        private By dashboardOption = By.XPath("//span[text()='Dashboard']");
        private By directoryOption = By.XPath("//span[text()='Directory']");
        private By maintenanceOption = By.XPath("//span[text()='Maintenance']");
        private By claimOption = By.XPath("//span[text()='Claim']");
        private By buzzOption = By.XPath("//span[text()='Buzz']");


        public dynamic goToMainMenu(MainMenu mainMenu)
        {
            Console.WriteLine($"Go to {mainMenu} page");    
            switch (mainMenu)
            {
                case MainMenu.Admin:
                    Common.ClickOnElement(adminOption);
                    return new AdminPage(driver);
                case MainMenu.Dashboard:
                    Common.ClickOnElement(dashboardOption);
                    return new DashboardPage(driver);
                default:
                    throw new ArgumentException($"Unsupported MainMenu value: {mainMenu}");
            }
        }

    }
}