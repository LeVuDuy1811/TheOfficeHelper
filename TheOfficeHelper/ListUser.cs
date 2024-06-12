using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOfficeHelper
{
    public class ListUser
    {
        private static ListUser instance;

        private List<user> listAccountUser;

        public List<user> ListAccountUser { get => listAccountUser; set => listAccountUser = value; }
        public static ListUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new ListUser();
                return instance;
                
            }
            set => instance = value;
        }

        private  ListUser()
        {
            listAccountUser = new List<user>();
            listAccountUser.Add(new user("Admin", "123456",true));
            listAccountUser.Add(new user("Employee", "123456",false));
        }
    }
}
