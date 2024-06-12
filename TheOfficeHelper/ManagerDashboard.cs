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
    public partial class ManagerDashboard : Form
    {
        public bool isExit = true;
        public ManagerDashboard()
        {
            InitializeComponent();
        }
        MainFrom f1 = new MainFrom();
        AssignmentManager f3 = new AssignmentManager();

        SqlConnection cn = new SqlConnection(@"Data Source=LYNOLEE;Initial Catalog=AssignmentDB;Integrated Security=True");//Connection string varies with each PC and DB

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

        private void btnCreateAssignment_Click(object sender, EventArgs e)
        {
            f3.Show();
            this.Hide();
        }

        private void ManagerDashboard_Load(object sender, EventArgs e)
        {
            display();
        }

        private void btnCreateEmployee_Click(object sender, EventArgs e)
        {
            f1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void ManagerDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void ManagerDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChooseFrom chooseFrom = new ChooseFrom();
            chooseFrom.Show();
        }
    }
}

