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
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager)
           : base(manager)
        {
        }
        public ProjectHelper Create(ProjectData project)
        {
            manager.Management.OpenManagePage();
            manager.Management.GoToMangeProjPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return this;
        }

        public void Remove(AccountData account, string projectId)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            client.mc_project_delete(account.Username, account.Password, projectId);
        }

        public void Remove(ProjectData project)
        {
            manager.Management.GoToProjectsPage();
            SelectProject(project);
            RemoveProject();
        }

        private void SelectProject(ProjectData project)
        {
            driver.FindElement(By.LinkText(project.Name)).Click();
        }

        public ProjectHelper Remove(AccountData account, int index)
        {
            manager.Management.OpenManagePage();
            manager.Management.GoToMangeProjPage();
            SelectProject(index);
            RemoveProject();
            SubmitProjectRemoving();
            return this;
        }
        public ProjectHelper RemoveProject()
        {
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            return this;
        }
        public ProjectHelper SubmitProjectRemoving()
        {
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            return this;

        }
        public ProjectHelper SelectProject(int index)
        {
            driver.FindElement(By.XPath("//i[@class='fa fa-puzzle-piece ace-icon']/../../..//tbody//tr[" + (index + 1) + "]/td[1]/a")).Click(); return this;
        }
        public ProjectHelper InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-white btn-round']")).Click();
            return this;
        }
        public ProjectHelper FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Id("project-name")).Click();
            driver.FindElement(By.Id("project-name")).Clear();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            return this;
        }

        public ProjectHelper SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Add Project']")).Click();
            return this;
        }

        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            manager.Management.OpenManagePage();
            manager.Management.GoToMangeProjPage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//i[@class='fa fa-puzzle-piece ace-icon']/../../..//tbody//tr"));
            foreach (IWebElement element in elements)
            {
                IWebElement cells = element.FindElements(By.TagName("td")).FirstOrDefault();
                ProjectData project = new ProjectData(cells.Text);
                projects.Add(project);
            }
            return projects;
        }
    }
}