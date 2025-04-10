using FodenApp.utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FodenApp.pages
{
    public class DashboardPage
    {
        private readonly IWebDriver driver;
        private By dashboardHeaderPage = By.XPath("//h6[text()='Dashboard']");
        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            Common.WaitForElementVisible(dashboardHeaderPage);
        }

    }
}