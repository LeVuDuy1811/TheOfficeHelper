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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        bool CheckLogin(string userName, string passWord)
        {
            for (int i = 0; i < ListUser.Instance.ListAccountUser.Count; i++)
            {
                if (userName == ListUser.Instance.ListAccountUser[i].UserName && passWord == ListUser.Instance.ListAccountUser[i].PassWord)
                {
                    Const.AccountType = ListUser.Instance.ListAccountUser[i].AccountType;
                    return true;
                }
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUsername.Text;
            string passWord = txtPassword.Text;
            if (CheckLogin(userName, passWord))
            {
                ChooseFrom chooseFrom = new ChooseFrom();
                chooseFrom.Show();
                this.Hide();
                chooseFrom.Logout += CF_Logout;
            }
            else
            {
                MessageBox.Show("Have something wrong. Please check username or password", "Error", MessageBoxButtons.OK);
                txtUsername.Focus();
                return;
            }

        }

        private void CF_Logout(object sender, EventArgs e)
        {
            (sender as ChooseFrom).isExit = false;
            (sender as ChooseFrom).Close();
            this.Show();
        }


        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cksShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cksShowPass.Checked)
                txtPassword.UseSystemPasswordChar = false;
            if (!cksShowPass.Checked)
                txtPassword.UseSystemPasswordChar = true;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
            {
                this.btnLogin.PerformClick();
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    } 

    }

