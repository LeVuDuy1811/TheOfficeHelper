using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheOfficeHelper
{
    public partial class EmployeeDashboard : Form
    {
        public bool isExit = true;
        public EmployeeDashboard()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=LYNOLEE;Initial Catalog=AssignmentDB;Integrated Security=True");

        AssignmentEmployee f5 = new AssignmentEmployee();
        void display()
        {
            cn.Open();
            SqlCommand c = new SqlCommand("exec DisplayAssignment_TP", cn);
            c.ExecuteNonQuery();
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            cn.Close();

        }

        public int restraint = 0;

        private void btnShow_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnUpAssigment_Click(object sender, EventArgs e)
        {
            f5.ShowDialog();
            this.Hide();
        }

        private void btnCheckAss_Click(object sender, EventArgs e)
        {
            f5 = new AssignmentEmployee();
            f5.Show();
            this.Hide();
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            display();
        }

        private void EmployeeDashboard_Load(object sender, EventArgs e)
        {

        }

        private void EmployeeDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }

        }

        private void EmployeeDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChooseFrom back = new ChooseFrom();
            back.Show();
        }
    }
}
