using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;



namespace mantis_tests
{
        public class ApplicationManager : TestBase
    {

        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public LoginHelper loginHelper;
        public ManagementHelper managementHelper;
        public ProjectHelper projectHelper;

        public AdminHelper Admin { get; set; }
        public APIHelper API { get; set; }

        private new static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public ApplicationManager() 
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            baseURL = "http://localhost/mantisbt-2.20.0";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);

            loginHelper = new LoginHelper(this);
            managementHelper = new ManagementHelper(this, baseURL);
            projectHelper = new ProjectHelper(this);
            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);

        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }
        public IWebDriver Driver
        {
            get
            {
                return driver; 
            }
        }
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }
        public ManagementHelper Management
        {
            get
            {
                return managementHelper;
            }
        }
        public ProjectHelper Projects
        {
            get
            {
                return projectHelper;
            }
        }
    }
}
