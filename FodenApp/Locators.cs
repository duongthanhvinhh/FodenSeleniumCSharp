using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework.Legacy;
using FodenApp.utilities;

namespace FodenApp;

public class Locators : Base
{

    [Test]
    [Retry(1)]
    [Category("Smoke")]
    public void VerifyThatUserCanLoginSuccessfully()
    {
        WebDriverWait wait;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='username']")));
        driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("Admin");
        driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("admin123");
        driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        // Assert.That(driver.FindElement(By.XPath("//h6[text()='Dashboard']")).Displayed);
        // wait.Until(d => d.FindElement(By.XPath("//p[text()='Punched In']")).Displayed);
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[text()='Punched Out']")));
        IWebElement ele = driver.FindElement(By.XPath("//p[text()='Punched Out']"));
        String className = ele.GetAttribute("class");
        Assert.That(className, Is.EqualTo("oxd-text oxd-text--p orangehrm-attendance-card-state"));
        // ClassicAssert.AreEqual(className, "oxd-text oxd-text--p orangehrm-attendance-card-state"); //Not recommended to use anymore
    }

}