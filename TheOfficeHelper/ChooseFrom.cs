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
    public partial class ChooseFrom : Form
    {
        public bool isExit = true;
        public event EventHandler Logout;
        public ChooseFrom()
        {
            InitializeComponent();
        }

        

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        #region
        private void ChooseFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void ChooseFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void ChooseFrom_Load(object sender, EventArgs e)
        {
            Decentralization();
        }
        #endregion
        #region method
        void Decentralization ()
        {
            if (Const.AccountType == false)
            {
                btnAM.Enabled =btnManagerDashboard.Enabled =false;
            }
        }
        #endregion
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            MainFrom mainFrom = new MainFrom();
            mainFrom.Show();
            this.Hide();
        }

        private void btnAM_Click(object sender, EventArgs e)
        {
            AM aM= new AM();
            aM.ShowDialog();
            
        }

        private void btnManagerDashboard_Click(object sender, EventArgs e)
        {
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
            this.Hide();
        }

        private void btnEmployeeDashboard_Click(object sender, EventArgs e)
        {
            EmployeeDashboard employeeDashboard = new EmployeeDashboard();
            employeeDashboard.Show();
            this.Hide();
        }
    }
}
