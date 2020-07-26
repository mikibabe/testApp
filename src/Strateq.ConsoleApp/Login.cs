using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Strateq.ConsoleApp.AppCore;
using System;
using System.Collections.Generic;
using static Strateq.ConsoleApp.AppCore.TestForm;

namespace dotnet
{
    class UserLogin
    {
        const string Login_Url = "http://dev03.37degrees.us/STQHIS/OSC";
        const string Login_FormElemTemp = "{0}";
        const string Login_ButtonId = "submitForm";

        public static ChromeDriver Execute()
        {
            var driver = new ChromeDriver(); 
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Common.GoToUrl(driver, Login_Url); 

            var formValList = Login_GetFormValueList();
            TestForm.FillText(driver, formValList, Login_FormElemTemp);
             
            Common.ClickButton(driver, Login_ButtonId);

            return driver;
        }

        static List<TestForm.FormData> Login_GetFormValueList()
        {
            var formValList = new List<TestForm.FormData>();

            var formVal = new TestForm.FormData
            {
                Id = "user_login_name",
                Text = "dan3" 
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "user_password",
                Text = "Password!23"
            };
            formValList.Add(formVal);

            formVal = new TestForm.FormData
            {
                Id = "location_role_id",
                Text = "13",
                ControlTypeId = ControlType.Select
            };
            formValList.Add(formVal); 

            return formValList;
        }

    }
}
