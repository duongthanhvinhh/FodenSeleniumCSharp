using FodenApp.utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FodenApp.pages
{
    public class AdminPage
    {
        private readonly IWebDriver driver;
        private By adminHeaderpage = By.XPath("//h6[text()='Admin']");
        public AdminPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            Common.WaitForElementVisible(adminHeaderpage);
        }

    }
}