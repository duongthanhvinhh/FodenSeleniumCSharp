﻿using OpenQA.Selenium;
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

    [Parallelizable(ParallelScope.All)] // Runs all test methods in parallel
    // [Parallelizable(ParallelScope.Self)] // Define parallelizable Self for all classes under a folder test, when run all tests under
    // that folder, all tests in classes will be run in parallel
    public class LoginTests : DriverManager
    {

        [Test]
        [Retry(1)]
        [Category("Smoke")]
        public void VerifyThatUserCanLoginSuccessfully()
        {
            LoginPage loginPage = new LoginPage(getDriver());
            Navbar navbar = loginPage.ValidLogin("Admin", "admin123");
            AdminPage adminPage = navbar.goToMainMenu(enums.MainMenu.Admin);
            // Assert.That(driver.FindElement(By.XPath("//h6[text()='Dashboard']")).Displayed);
            // wait.Until(d => d.FindElement(By.XPath("//p[text()='Punched In']")).Displayed);
            // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//p[text()='Punched Out']")));
            // IWebElement ele = driver.FindElement(By.XPath("//p[text()='Punched Out']"));
            // String className = ele.GetAttribute("class");
            // Assert.That(className, Is.EqualTo("oxd-text oxd-text--p orangehrm-attendance-card-state"));
            // ClassicAssert.AreEqual(className, "oxd-text oxd-text--p orangehrm-attendance-card-state"); //Not recommended to use anymore
        }

        [Test, TestCaseSource(nameof(AddTestDataConfig))]
        [Retry(1)]
        [Category("Smoke")]
        public void VerifyThatNonExistUserOrInvalidPasswordCanNotLogin(string username, string password)
        {
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.InvalidLogin(username, password);
            loginPage.VerifyLoginFailed();
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig(){
            JsonReader jsonReader = Common.getJsonReader("testdata/testData.json");
            yield return new TestCaseData(jsonReader.GetValue<string>("invalid_username"), jsonReader.GetValue<string>("password"));
            yield return new TestCaseData(jsonReader.GetValue<string>("username"), jsonReader.GetValue<string>("invalid_password"));
            yield return new TestCaseData(jsonReader.GetValue<string>("invalid_username"), jsonReader.GetValue<string>("invalid_password"));
        }

    }
}