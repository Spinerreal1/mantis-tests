using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {

        [Test]
        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData("Test_Project");

            List<ProjectData> oldProjects = app.Projects.GetProjectsList();

            app.Projects.Create(project);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}