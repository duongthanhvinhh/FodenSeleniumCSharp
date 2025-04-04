using System.Linq.Expressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace FodenApp.utilities
{

    public static class Common
    {


        static private bool acceptNextAlert = true;

        static public bool IsElementPresent(By by)
        {
            try
            {
                DriverManager.getDriver().FindElement(by);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public bool IsElementPresent(IWebElement element)
        {
            try
            {
                SetfocusOnIWebElement(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        static public bool IsAlertPresent()
        {
            try
            {
                DriverManager.getDriver().SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        static public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = DriverManager.getDriver().SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        static public void AcceptAndCloseAlert()
        {
            try
            {
                IAlert alert = DriverManager.getDriver().SwitchTo().Alert();
                //string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                //return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        static public void SwitchToWindow(Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in DriverManager.getDriver().WindowHandles)
            {
                DriverManager.getDriver().SwitchTo().Window(handle);
                if (predicate(DriverManager.getDriver()))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with condition: '{0}'", predicateExp.Body));
        }

        static public void NavigateToURL(string url)
        {
            DriverManager.getDriver().Navigate().GoToUrl(url);
        }

        static public void ClickOnElement(By locator)
        {
            DriverManager.getDriver().FindElement(locator).Click();
        }

        static public IWebElement FindElement(By locator)
        {
            IWebElement element = DriverManager.getDriver().FindElement(locator);
            return element;
        }

        static public SelectElement Dropdown(IWebElement element)
        {
            SelectElement select = new SelectElement(element);
            return select;
        }

        static public SelectElement Dropdown(By selectDrpDwnLocator)
        {
            SelectElement select = new SelectElement(DriverManager.getDriver().FindElement(selectDrpDwnLocator));
            return select;
        }

        static public string GetText(By locator)
        {
            string txtValue = DriverManager.getDriver().FindElement(locator).Text;
            return txtValue;
        }

        static public string GetAttributeValue(By locator, string attributeName)
        {
            string txtValue = DriverManager.getDriver().FindElement(locator).GetAttribute(attributeName);
            return txtValue;
        }

        static public void EnterText(By locator, string requiredText)
        {
            DriverManager.getDriver().FindElement(locator).Clear();
            DriverManager.getDriver().FindElement(locator).SendKeys(requiredText);
        }


        static public void SetfocusOnIWebElement(IWebElement element)
        {
            ActionBuilder().MoveToElement(element).Build().Perform();
        }
        static public void SetfocusAndClickOnIWebElement(IWebElement element)
        {
            ActionBuilder().MoveToElement(element, 0, 0).Click().Build().Perform();
        }

        static public void ImplicitWait(int seconds = 10)
        {
            DriverManager.getDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }

        static public WebDriverWait Wait()
        {
            WebDriverWait Wait = new WebDriverWait(DriverManager.getDriver(), TimeSpan.FromSeconds(30));
            return Wait;
        }

        static public void WaitForElementVisible(By locator, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.getDriver(), TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        static public void WaitForElementToGetInVisible(By locator, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.getDriver(), TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        static public void WaitForElementIsClickable(By locator, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.getDriver(), TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        static public void WaitForElementIsClickable(IWebElement element, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.getDriver(), TimeSpan.FromSeconds(seconds));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        static public Actions ActionBuilder()
        {
            Actions ActBuilder = new Actions(DriverManager.getDriver());
            return ActBuilder;
        }


        static public string TakeScreenshot(string screenshotBaseLocation)
        {

            //Folder Name for saving screenshots
            string screenshotFolderName = "Screenshots " + DateTime.Now.ToString("yyyy_MM_dd");

            //Directory path for saving screenshots
            string directoryPath = screenshotBaseLocation + screenshotFolderName;

            if (Directory.Exists(directoryPath) == false)
            {
                DirectoryInfo di = Directory.CreateDirectory(directoryPath);
            }

            //Screenshot path for returning the complete screenshot URL with its name
            string screnshotPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath + @"\");

            string tcName = TestContext.CurrentContext.Test.Name.Replace('"', '\'').Replace(";", "-").Replace("/", "_");

            int tcNameLength = TestContext.CurrentContext.Test.Name.Replace('"', '\'').Replace(";", "-").Replace("/", "_").Length;

            if (tcNameLength > 50)
            {
                tcName = tcName.Substring(0, 50) + "...";
            }


            //Take Screenshot and save it @ specified location
            #region TakeScreenshotAndSaveIt


            ITakesScreenshot screenshotDriver = DriverManager.getDriver() as ITakesScreenshot;

            Screenshot screenshot = screenshotDriver.GetScreenshot();

            String fp = screnshotPath + tcName + "Screenshot" + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_tt") + ".png";

            //screenshot.SaveAsFile(fp, ImageFormat.Png);

            //screenshot.SaveAsFile(fp, ScreenshotImageFormat.Png);

            #endregion

            #region Return screenshot path


            string machineName = System.Environment.MachineName.ToString();
            string fpWithHostName = "\\\\" + machineName + "\\" + fp;

            var uri = new System.Uri(fpWithHostName);
            var converted = uri.AbsoluteUri.Replace("/", "\\");
            return converted;
            #endregion
        }

        static public List<IWebElement> GetCollection(By locator)
        {
            List<IWebElement> collection = DriverManager.getDriver().FindElements(locator).ToList();
            return collection;
        }

        static public List<IWebElement> GetCollectionToList(By locator)
        {
            List<IWebElement> webElementList = DriverManager.getDriver().FindElements(locator).ToList();
            return webElementList;
        }

        static public List<string> GetIwebElementTextToStringList(By locator)
        {
            List<IWebElement> allitems = DriverManager.getDriver().FindElements(locator).ToList();

            List<string> allIWebElemetstostringList = GetAllItemsToStringList(allitems);
            return allIWebElemetstostringList;
        }

        static public List<string> GetAllItemsToStringList(List<IWebElement> allElements, string textFromWhereToTrim = "AmolDakhane")
        {
            List<string> actualElements = new List<string>();

            for (int i = 0; i < allElements.Count(); i++)
            {
                IWebElement element = allElements[i];

                string ElementText = element.Text.Trim();
                int index = ElementText.IndexOf(textFromWhereToTrim);
                if (index > 0)
                {
                    ElementText = ElementText.Substring(0, index);
                }
                actualElements.Add(ElementText.Trim());
            }
            return actualElements;
        }

        static public List<string> GetAllItemsToStringListExcludingBlank(By locator, string textFromWhereToTrim = "FodenDuong")
        {

            List<IWebElement> webElementList = DriverManager.getDriver().FindElements(locator).ToList();

            List<string> actualElements = new List<string>();

            for (int i = 0; i < webElementList.Count(); i++)
            {

                IWebElement element = webElementList[i];

                string ElementText = element.Text.Trim();
                if (ElementText.Length > 0)
                {
                    int index = ElementText.IndexOf(textFromWhereToTrim);
                    if (index > 0)
                    {
                        ElementText = ElementText.Substring(0, index);
                    }
                    actualElements.Add(ElementText.Trim());
                    //loma
                }
            }

            if (actualElements.Last().Length < 1)
            {
                actualElements.RemoveAt(actualElements.Count - 1);
            }
            return actualElements;
        }

        static public List<string> GetCollectionFromPageToStringList(By locator, string textFromWhereToTrim = "AmolDakhane")
        {

            List<IWebElement> webElementList = DriverManager.getDriver().FindElements(locator).ToList();


            List<string> actualElements = new List<string>();

            for (int i = 0; i < webElementList.Count(); i++)
            {
                IWebElement element = webElementList[i];

                string ElementText = element.Text.Trim();
                int index = ElementText.IndexOf(textFromWhereToTrim);
                if (index > 0)
                {
                    ElementText = ElementText.Substring(0, index);
                }
                actualElements.Add(ElementText.Trim());

            }
            return actualElements;
        }

        static public List<string> GetCollectionFromPageToStringListWit(By locator, string textFromWhereToTrim = "AmolDakhane")
        {

            List<IWebElement> webElementList = DriverManager.getDriver().FindElements(locator).ToList();

            List<string> actualElements = new List<string>();

            for (int i = 0; i < webElementList.Count(); i++)
            {
                IWebElement element = webElementList[i];

                string ElementText = element.Text.Trim();
                int index = ElementText.IndexOf(textFromWhereToTrim);
                if (index > 0)
                {
                    ElementText = ElementText.Substring(0, index);
                }
                actualElements.Add(ElementText.Trim());
            }

            if (actualElements.Last().Length < 1)
            {
                actualElements.RemoveAt(actualElements.Count - 1);
            }
            return actualElements;
        }

        public static List<string> SplitEachItemInStringCollectionAndAddtoList(List<string> collection, string[] splitByCharacter)
        {
            List<string> splittedItemList = new List<string>();

            foreach (string item in collection)
            {
                string[] splittedTerms = item.Split(splitByCharacter, StringSplitOptions.None);

                foreach (string term in splittedTerms)
                {
                    splittedItemList.Add(term);
                }

            }
            return splittedItemList;
        }

        static public List<string> GetValuesOfSpecificColumnByIndexFromTable(By tableIdentifier, int columnNumber)
        {
            IWebElement tableName = DriverManager.getDriver().FindElement(tableIdentifier);
            List<IWebElement> tr_tableElements = tableName.FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[columnNumber].Text.Trim());

            }
            return valuesFromSelectedTableColumn;
        }

        static public List<string> GetValuesOfSpecificColumnByIndexFromTable(IWebElement tableIdentifier, int columnNumber)
        {

            List<IWebElement> tr_tableElements = tableIdentifier.FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[columnNumber].Text.Trim());

            }
            return valuesFromSelectedTableColumn;
        }

        static public List<string> GetValuesOfColumnByNameFromTable(IWebElement tableIdentifier, string columnName)
        {
            List<IWebElement> tableHeaders = tableIdentifier.FindElements(By.CssSelector("thead>tr>th")).ToList();

            int indexOfRequiredColumnName = tableHeaders.FindIndex(x => x.Text.Contains(columnName));

            List<IWebElement> tr_tableElements = tableIdentifier.FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[indexOfRequiredColumnName].Text.Trim());

            }
            return valuesFromSelectedTableColumn;

        }

        static public int GetIndexOfRowContainingSpecificText(By locatorForRows, string stringToBeSearched)
        {
            List<IWebElement> tableRows = GetCollection(locatorForRows).ToList();
            int indexOfRequiredText = tableRows.FindIndex(x => x.Text.Contains(stringToBeSearched));
            return indexOfRequiredText;
        }

        static public int GetIndexOfTableRowContainingSpecificText(By tableIdentifier, string stringToBeSearched)
        {
            IWebElement table = DriverManager.getDriver().FindElement(tableIdentifier);

            List<IWebElement> tableRows = table.FindElements(By.CssSelector("tbody>tr")).ToList();
            int indexOfRequiredText = tableRows.FindIndex(x => x.Text.Contains(stringToBeSearched));
            return indexOfRequiredText;
        }

        public static string GetValueFromSpecificCellFromTable(By tableIdentifier, int rowIndex, int columnIndex)
        {
            IWebElement table = DriverManager.getDriver().FindElement(tableIdentifier);
            return table.FindElements(By.CssSelector("tbody>tr"))[rowIndex].FindElements(By.CssSelector("td"))[columnIndex].Text;
        }

        public static string GetValueFromSpecificCellFromTable(IWebElement tableIdentifier, int rowIndex, int columnIndex)
        {
            return tableIdentifier.FindElements(By.CssSelector("tbody>tr"))[rowIndex].FindElements(By.CssSelector("td"))[columnIndex].Text;
        }

        public static void ClickOnCellTextFromTable(string txtTOBeClicked)
        {
            DriverManager.getDriver().FindElement(By.XPath("//tr[td[contains(text(),'" + txtTOBeClicked + "')]]")).Click();
        }

        public static void ClickOnSpecificCellFromTable(By tableIdentifier, int rowIndex, int colIndex)
        {
            IWebElement table = DriverManager.getDriver().FindElement(tableIdentifier);
            table.FindElement(By.XPath("//tbody/tr[" + rowIndex + "]/td[" + colIndex + "]")).Click();
        }

        public static int GetNumberOfRowsInTable(By tableLocator)
        {
            int numberOfRowsInTable = DriverManager.getDriver().FindElement(tableLocator).FindElements(By.CssSelector("tbody>tr")).Count;
            return numberOfRowsInTable;
        }

        public static int GetNumberOfRowsInTable(IWebElement tableElement)
        {
            int numberOfRowsInTable = tableElement.FindElements(By.CssSelector("tbody>tr")).Count;
            return numberOfRowsInTable;
        }

        static public void SwitchToDilog()
        {
            DriverManager.getDriver().SwitchTo().Frame(DriverManager.getDriver().FindElement(By.ClassName("ms-dlgFrame")));
        }

        static public void DragAndDrop(IWebElement sourceElement, IWebElement destinationElement)
        {
            ActionBuilder().ClickAndHold(sourceElement)
            .MoveToElement(destinationElement)
            .Release(destinationElement)
            .Build()
            .Perform();
        }

        public static void SelectByValueFromSelectTagDropDown(this IWebElement element, string ItemValue)
        {
            SelectElement SelectTagddl = new SelectElement(element);

            SelectTagddl.SelectByText(ItemValue);
        }

        static public void SelectFromDrpDwnIgnoringStaleElementException(IWebElement Element, string option, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    SelectByValueFromSelectTagDropDown(Element, option);
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
        }

        static public void SelectFromListDrpDwnIgnoringStaleElementException(IList<IWebElement> IWebElementsCollection, string itemName, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    if (itemName.Length >= 1)
                    {
                        foreach (IWebElement item in IWebElementsCollection)
                        {

                            if (item.Text == itemName)
                            {
                                Thread.Sleep(1500);
                                item.Click();
                                break;
                            }
                        }
                    }
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
        }

        static public void ClickOnElementIgnoringStaleElementException(IWebElement Element, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.Click();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
        }

        static public void ClearTextIgnoringStaleElementException(IWebElement Element, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.Clear();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
        }

        static public void EnterTextIgnoringStaleElementException(IWebElement Element, string text, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.SendKeys(text);
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
        }

        static public void SelectFromListDropDown(IList<IWebElement> IWebElementsCollection, string itemName)
        {
            if (itemName.Length >= 1)
            {
                foreach (IWebElement item in IWebElementsCollection)
                {

                    if (item.Text == itemName)
                    {
                        Thread.Sleep(1500);
                        item.Click();
                        break;
                    }
                }
            }
        }


        static public void ClickOnElementUsingJavaScript(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DriverManager.getDriver();
            jse.ExecuteScript("arguments[0].click();", element);

        }


        static public void ScrollToElementUsingJavaScript(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DriverManager.getDriver();
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", element);

        }
    }
}