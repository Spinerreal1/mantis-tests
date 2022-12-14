    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoveTests : AuthTestBase
    {
        [Test]
        public void ProjectRemoveTest()
        {
            AccountData account = new AccountData("administrator", "root");

            if (app.API.GetProjectsList(account).Count == 0)
            {
                app.API.CreateNewProject(account, new ProjectData(GenerateRandomString(10)));
            }

            List<ProjectData> oldProjects = app.API.GetProjectsList(account);

            ProjectData toBeRemoved = oldProjects[0];

            app.Projects.Remove(account, toBeRemoved.Id);

            List<ProjectData> newProjects = app.API.GetProjectsList(account);
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}