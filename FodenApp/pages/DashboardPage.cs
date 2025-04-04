using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FodenApp.pages
{
    public class DashboardPage
    {
        private readonly IWebDriver driver;

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

    }
}