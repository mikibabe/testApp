using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Strateq.ConsoleApp.AppCore
{
    public static class Common
    { 
        public static void ScrollPageDown(IWebElement webelement)
        {
            webelement.SendKeys(Keys.Down);
            Thread.Sleep(500);
        }

        public static void ScrollPageIntoView(IJavaScriptExecutor driver, IWebElement webelement)
        {
            driver.ExecuteScript("arguments[0].scrollIntoView(true);", webelement);
            Thread.Sleep(500);
        }

        public static void ScrollVerticalByPixel(ChromiumDriver driver, int px)
        { 
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0," + px.ToString() + ")");
        }
         
        public static void SelectValue(ChromiumDriver driver, string targetId, string targetValue)
        {

            var education = driver.FindElementById(targetId);
            var selectElement = new SelectElement(education);

            //select by value
            selectElement.SelectByValue(targetValue); 
        }
         
        public static void SelectContentTopMenu(ChromiumDriver driver, string linkText)
        { 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
            driver.FindElement(By.LinkText(linkText)).Click();
        }

        public static void EnterText(ChromiumDriver driver, string id, string txt)
        {
            driver.FindElementById(id).SendKeys(txt);
            Thread.Sleep(3000);
        }

        public static void ClickButton(ChromiumDriver driver, string id)
        {
            driver.FindElement(By.Id(id)).Click();
            Thread.Sleep(3000);
        }
         
        public static void GoToUrl(ChromiumDriver driver, string link)
        {
            driver.Navigate().GoToUrl(link);
        }
    }

    public static class TestForm
    {
        public class FormData
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public bool IsTab { get; set; }
            public int ControlTypeId { get; set; } 
        }

        public static class ControlType
        {
            public static int Text = 0;
            public static int Select = 1;
            public static int Button = 2;
        }

        public static void FillText(ChromiumDriver driver, List<FormData> formDataList, string formElemTemp)
        {
            foreach (FormData fv in formDataList)
            {
                var elemId = String.Format(formElemTemp, fv.Id);
                Boolean isElemEmpty = driver.FindElement(By.Id(elemId)).Size.IsEmpty;

                if (isElemEmpty)
                {
                    Common.ScrollVerticalByPixel(driver, 300);
                }

                if (fv.ControlTypeId == ControlType.Select)
                {
                    Common.SelectValue(driver, elemId, fv.Text);
                }
                else if (fv.ControlTypeId == ControlType.Button)
                {
                    Actions action = new Actions(driver);
                    var el = driver.FindElementById(elemId); 
                    Common.ScrollPageDown(el);
                    action.MoveToElement(el).Click().Perform();
                }
                else
                {
                    Common.EnterText(driver, elemId, fv.Text);
                }

                if (fv.IsTab)
                {
                    driver.FindElement(By.Id(elemId)).SendKeys(Keys.Tab);
                    Thread.Sleep(2000); //check
                }
                Thread.Sleep(500);
            }
        }

    }
     
    public static class JqGrid
    {
        public static void AddNewRowThenSelect(ChromiumDriver driver, string addBtnId, string itemRowSelector, string[] textArr)
        {
            var rowIndex = 0;
            foreach (string txt in textArr)
            {
                Actions action = new Actions(driver);
                var addBtn = driver.FindElement(By.Id(addBtnId));
                action.MoveToElement(addBtn).Click().Perform();
                Thread.Sleep(3000);

                string itemRowSelectorByIndex = String.Format(itemRowSelector, rowIndex);
                var itemRow = driver.FindElement(By.CssSelector(itemRowSelectorByIndex));

                Common.ScrollVerticalByPixel(driver, 300);

                action = new Actions(driver);
                action.MoveToElement(itemRow).Click().Perform();
                Thread.Sleep(2000);

                action = new Actions(driver);
                action.MoveToElement(itemRow).SendKeys(txt).Build().Perform();
                Thread.Sleep(3000);

                action.Click(itemRow).SendKeys(Keys.ArrowDown).SendKeys(Keys.Enter).Build().Perform();
                Thread.Sleep(3000);
                 
                Common.ScrollVerticalByPixel(driver, -300);
                Thread.Sleep(3000);

                rowIndex++;
            }
        }
    }

}
