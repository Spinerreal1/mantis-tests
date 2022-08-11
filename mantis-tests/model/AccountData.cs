using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData
    {
        public string Id { get;  set; }                  
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public AccountData(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public AccountData()
        {
        }
    }
}
