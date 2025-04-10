using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace FodenApp.pages
{
    public class LoginPage
    {

        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@name='username']")]
        private IWebElement usernameInput;

        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        private IWebElement passwordInput;

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        private IWebElement submitBtn;

        public Navbar ValidLogin(string username, string password){
            usernameInput.SendKeys(username);
            passwordInput.SendKeys(password);
            submitBtn.Click();
            return new Navbar(driver);
        }
    }
}