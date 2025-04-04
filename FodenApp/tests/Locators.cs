using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework.Legacy;
using FodenApp.utilities;
using FodenApp.pages;

namespace FodenApp.tests
{

    public class Locators : DriverManager
    {

        [Test]
        [Retry(1)]
        [Category("Smoke")]
        public void VerifyThatUserCanLoginSuccessfully()
        {
            WebDriverWait wait;
            // wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='username']")));
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.getUsernameInput().SendKeys("Admin");
            loginPage.getPasswordInput().SendKeys("admin123");
            loginPage.getSubmitButton().Click();
            // Assert.That(driver.FindElement(By.XPath("//h6[text()='Dashboard']")).Displayed);
            // wait.Until(d => d.FindElement(By.XPath("//p[text()='Punched In']")).Displayed);
            // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[text()='Punched Out']")));
            // IWebElement ele = driver.FindElement(By.XPath("//p[text()='Punched Out']"));
            // String className = ele.GetAttribute("class");
            // Assert.That(className, Is.EqualTo("oxd-text oxd-text--p orangehrm-attendance-card-state"));
            // ClassicAssert.AreEqual(className, "oxd-text oxd-text--p orangehrm-attendance-card-state"); //Not recommended to use anymore
        }

    }
}