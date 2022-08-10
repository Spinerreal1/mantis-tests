using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;
        public AdminHelper(ApplicationManager manager) : base(manager) 
        {
            this.baseUrl = baseUrl;
        }

        public AdminHelper(ApplicationManager manager, string baseURL) : this(manager)
        {
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table tr.row-1, table tr.row-1"));
            foreach (IWebElement row in rows)
            {
               IWebElement link =  row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;

                accounts.Add(new AccountData()
                {
                    Name = name, 
                    Id = id
                });
            }
            return accounts;
        }
        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("input[value='Delete User']")).Click();
            driver.FindElement(By.CssSelector("input[value='Delete Account']")).Click();
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input.button")).Click();
            return driver;
        }
    }
}
