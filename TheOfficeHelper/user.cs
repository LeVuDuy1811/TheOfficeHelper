using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOfficeHelper
{
    public class user
    {
        private string userName;
        private string passWord;
        private bool accountType;
        private string v1;
        private string v2;

        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public bool AccountType { get => accountType; set => accountType = value; } 
        public user (string userName, string passWord, bool accountType)
        {
            this.UserName = userName;
            this.PassWord = passWord;
            this.AccountType = accountType;
        }

        public user(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }
}
