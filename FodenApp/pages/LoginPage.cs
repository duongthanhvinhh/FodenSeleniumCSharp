using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using FodenApp.utilities;

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
        private By usernameInput = By.XPath("//input[@name='username']");
        private By passwordInput = By.XPath("//input[@type='password']");
        private By submitBtn = By.XPath("//button[@type='submit']");
        private By loginFailedMessage = By.XPath("//p[text()='Invalid credentials']");

        public Navbar ValidLogin(string username, string password){
            Common.EnterText(usernameInput, username);
            Common.EnterText(passwordInput, password);
            Common.ClickOnElement(submitBtn);
            return new Navbar(driver);
        }

        public void InvalidLogin(string username, string password){
            Common.EnterText(usernameInput, username);
            Common.EnterText(passwordInput, password);
            Common.ClickOnElement(submitBtn);
        }

        public void VerifyLoginFailed(){
            Common.WaitForElementVisible(loginFailedMessage);
            Assert.That(Common.IsElementPresent(loginFailedMessage), "Message indicates login failed is not shown!");
        }
    }
}