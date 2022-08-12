using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ManagementHelper : HelperBase
    {
        private string baseURL;

        public ManagementHelper(ApplicationManager manager, string baseURL)
            : base(manager)
        {
            this.baseURL = baseURL;
        }
        public void GoToLoginPage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }
        public void OpenManagePage()
        {
            if (driver.Url == baseURL + "/mantisbt-2.25.2/account_page.php")
            {
                return;
            }
            driver.FindElement(By.CssSelector("i.fa-gears")).Click();
        }
        public void GoToMangeProjPage()
        {
            if (driver.Url == baseURL + "/addressbook/manage_overview_page.php")
            {
                return;
            }
            driver.FindElement(By.LinkText("Manage Projects")).Click();
        }
        public void GoToProjectsPage()
        {
            driver.Navigate().GoToUrl(baseURL + "manage_proj_page.php");
        }
    }
}