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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TheOfficeHelper
{
    public partial class AssignmentEmployee : Form
    {
        public bool isExit = true;
        public AssignmentEmployee()
        {
            InitializeComponent();
        }

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

        

        private void Form5_Load(object sender, EventArgs e)
        {
            display();
        }

        public int restraint = 0;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            cn.Open();
            int a = int.Parse(txtAEID.Text);
            SqlCommand c = cn.CreateCommand();
            c.CommandType = CommandType.StoredProcedure;

            c = new SqlCommand("select * from Assignment_TP where AssignmentID=" + a, cn); //Stored Procedure


            c.Parameters.AddWithValue("@AID", int.Parse(txtAEID.Text));

            SqlDataReader dr;
           dr = c.ExecuteReader();
            if (dr.Read())
            {
                txtAEName.Text = dr["AssignmentName"].ToString();


            }
            else MessageBox.Show("Found None");
            cn.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            display();
        }

        private void btnFinished_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("update Assignment_TP set AssignmentStatus=@AStatus where AssignmentID=@AID", cn);
            c.Parameters.AddWithValue("@AID", txtAEID.Text);
            c.Parameters.AddWithValue("@AStatus", "Finished"); //Update Status Attribute

            c.ExecuteNonQuery();
            MessageBox.Show("Updated");
            cn.Close();
            display();
        }

        private void btnOnHold_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("update Assignment_TP set AssignmentStatus=@AStatus where AssignmentID=@AID", cn);
            c.Parameters.AddWithValue("@AID", txtAEID.Text);
            c.Parameters.AddWithValue("@AStatus", "On-Hold");//Update Status Attribute

            c.ExecuteNonQuery();
            MessageBox.Show("Updated");
            cn.Close();
            display();
        }

        private void btnInPro_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("update Assignment_TP set AssignmentStatus=@AStatus where AssignmentID=@AID", cn);
            c.Parameters.AddWithValue("@AID", txtAEID.Text);
            c.Parameters.AddWithValue("@AStatus", "In-progress");//Update Status Attribute

            c.ExecuteNonQuery();
            MessageBox.Show("Updated");
            cn.Close();
            display();
        }

        private void AssignmentEmployee_Load(object sender, EventArgs e)
        {
            display();
        }

        private void AssignmentEmployee_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (restraint == 0)
            {
                EmployeeDashboard f4 = new EmployeeDashboard();
                f4.Activate();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Back_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployeeDashboard f4 = new EmployeeDashboard();
            f4.Show();
        }

        private void AssignmentEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
                
            }
        }
    }
}
