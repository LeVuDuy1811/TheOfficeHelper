using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheOfficeHelper
{
    public partial class AM : Form
    {
        List<string> listAccountType = new List<string>() { "admin", "employee" };
       
        
        int index = -1;
        public AM()
        {
            InitializeComponent();
        }

        void loadListUser()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ListUser.Instance.ListAccountUser;
            dataGridView1.Refresh();
        }
        
        private void AM_Load(object sender, EventArgs e)
        {
            CB.DataSource = listAccountType;
            loadListUser();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index= e.RowIndex;
            if (index < 0)
                return; 
            txtUS.Text= dataGridView1.Rows[index].Cells[0].Value.ToString();
            txtPass.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            switch (ListUser.Instance.ListAccountUser[index].AccountType)
            {
                case true: CB.Text = "admin";
                     break;
                case false: CB.Text = "employee";
                     break;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string userName=txtUS.Text;
            string passWord=txtPass.Text;
            bool accountType=false;

            switch (CB.Text)
            {
                case "admin":
                   accountType = true;
                    break;
                case "employee":
                    accountType=false;
                    break;
            }
            ListUser.Instance.ListAccountUser.Add(new user(userName, passWord, accountType));
            
            loadListUser();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (index < 0)
            {
                MessageBox.Show("Please choose one user");
                return;
            }
            string userName = txtUS.Text;
            string passWord = txtPass.Text;
            bool accountType = false;

            switch (CB.Text)
            {
                case "admin":
                    accountType = true;
                    break;
                case "employee":
                    accountType = false;
                    break;
            }
            ListUser.Instance.ListAccountUser[index].UserName = userName;
            ListUser.Instance.ListAccountUser[index].PassWord = passWord;
            ListUser.Instance.ListAccountUser[index].AccountType = accountType;

            loadListUser();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (index < 0)
            {
                MessageBox.Show("Please choose one user");
                return;
            }
            ListUser.Instance.ListAccountUser.RemoveAt(index);
            loadListUser();
        }

       
    }
}
