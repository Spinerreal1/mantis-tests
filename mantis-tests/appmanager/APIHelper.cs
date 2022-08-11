using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData projectData, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = projectData.Id;
            client.mc_issue_add(account.Username, account.Password, issue);
        }

        public void CreateNewProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient("MantisConnectPort", "http://localhost/mantisbt-2.25.2/api/soap/mantisconnect.php?WSDL");
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            client.mc_project_add(account.Username, account.Password, project);
        }
        public List<ProjectData> GetProjectsList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient("MantisConnectPort", "http://localhost/mantisbt-2.25.2/api/soap/mantisconnect.php?WSDL");
            Mantis.ProjectData[] priojectsback = client.mc_projects_get_user_accessible(account.Username, account.Password);
            List<ProjectData> projects = new List<ProjectData>();

            foreach (Mantis.ProjectData projectback in priojectsback)
            {
                ProjectData project = new ProjectData(projectback.name);
                projects.Add(project);
            }

            return projects;
        }
    }
}