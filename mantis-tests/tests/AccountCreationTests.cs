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
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]

        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localfile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localfile);
            }
            
        }
        
        [Test]
        public void TestAccountRegistration()
        {

            AccountData account = new AccountData()
            {
                Username = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain"
            };
            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x => x.Username == account.Username);
            if (existingAccount != null) 
            {
                app.Admin.DeleteAccount(existingAccount);
            }



            app.Registration.Register(account);
        }
        [TestFixtureTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}
    