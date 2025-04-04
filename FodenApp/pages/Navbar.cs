using FodenApp.enums;
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

        [FindsBy(How = How.XPath, Using = "//span[text()='Admin']")]
        private IWebElement adminOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='PIM']")]
        private IWebElement PIMOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Leave']")]
        private IWebElement leaveOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Time']")]
        private IWebElement timeOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Recruitment']")]
        private IWebElement recruitmentOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='My Info']")]
        private IWebElement myInfoOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Performance']")]
        private IWebElement performanceOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Dashboard']")]
        private IWebElement dashboardOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Directory']")]
        private IWebElement directoryOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Maintenance']")]
        private IWebElement maintenanceOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Claim']")]
        private IWebElement claimOption;

        [FindsBy(How = How.XPath, Using = "//span[text()='Buzz']")]
        private IWebElement buzzOption;


        public void goToMainMenu(MainMenu mainMenu)
        {
            switch (mainMenu)
            {
                case MainMenu.Admin:
                    Console.WriteLine("TODO: Implement logic here to go to each item in navbar");
                    break;

                default:
                    break;
            }
        }

    }
}