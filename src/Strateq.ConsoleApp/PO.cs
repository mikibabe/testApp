using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Strateq.ConsoleApp.AppCore;
using System;
using System.Collections.Generic;
using System.Threading;
using static Strateq.ConsoleApp.AppCore.TestForm;

namespace dotnet
{
    class PO
    {
        const string pageTitle = "Purchase Order";
        const string PO_ItemRowTemp = "#row{0}purchaseorders_jqxgrid > div:nth-child(1)";
        const string PO_AddRowBtn = "purchaseorderssomgr_addrow_btn";
        const string PO_FormElemTemp = "purchaseordersomgr_{0}";

        static void Main(string[] args)
        {   
            string[] PO_SelectCode = new string[] { "PHAREGTAB001", "PHAREGTAB002", "PHAREGTAB003", "PHAREGTAB004", "PHAREGTAB005" };

            var myDefaultTimeOut = TimeSpan.FromSeconds(10); 
            ChromeDriver driver = UserLogin.Execute();

            Common.SelectContentTopMenu(driver, pageTitle);

            var formValList = PO_GetFormValueList(); 
            TestForm.FillText(driver, formValList, PO_FormElemTemp); 

            Common.ScrollVerticalByPixel(driver, 300); 
            Thread.Sleep(500);

            var el2 = driver.FindElement(By.Id(PO_AddRowBtn));
            Common.ScrollPageDown(el2); 
            Thread.Sleep(2000);

            PO_AddNewRowThenSelect(driver, PO_SelectCode); 

        }

        static List<TestForm.FormData> PO_GetFormValueList()
        {
            var formValList = new List<TestForm.FormData>();

            var formVal = new TestForm.FormData
            {
                Id = "vendor",
                Text = "8P0001",
                IsTab = true
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "address",
                Text = "No 12"
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "address2",
                Text = "Jalan Bunga Raya 4"
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "address3",
                Text = "Batu 3 1/2"
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "address4",
                Text = "56000, KL"
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "remarks2",
                Text = "Attention, This is very urgent purchase order, no delay."
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "very_urgent",
                ControlTypeId = ControlType.Button
            };
            formValList.Add(formVal);

            return formValList;
        }

        public static void PO_AddNewRowThenSelect(ChromeDriver driver, string[] textArr)
        {
            JqGrid.AddNewRowThenSelect(driver, PO_AddRowBtn, PO_ItemRowTemp, textArr);
        }
          
    }
}
