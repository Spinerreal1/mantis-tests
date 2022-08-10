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
            if (app.Projects.GetProjectsList().Count == 0)
            {
                app.Projects.Create(new ProjectData("Test_Project"));
            }

            List<ProjectData> oldProjects = app.Projects.GetProjectsList(); ;

            app.Projects.Remove(0);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}