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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TheOfficeHelper
{
    public partial class AssignmentManager : Form
    {
        public bool isExit = true;
        public AssignmentManager()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(@"Data Source=LYNOLEE;Initial Catalog=AssignmentDB;Integrated Security=True");//Connection string varies with each PC and DB
        //Similar to CRUD, change attribute.
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



        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            int a = int.Parse(txtAID.Text);
            string name = txtAName.Text;
            string status = comboBox1.SelectedItem.ToString();
            string startdate = DateTime.Parse(dateTimePicker1.Text).ToString();
            string enddate = DateTime.Parse(dateTimePicker2.Text).ToString();
            SqlCommand c = new SqlCommand("Insert Into Assignment_TP(AssignmentID, AssignmentName, AssignmentStatus, AssignmentStartDate, AssignmentEndDate) values (@AID, @AName, @AStatus, @AStartDate, @AEndDate)", cn);
            c.Parameters.AddWithValue("@AID", a);
            c.Parameters.AddWithValue("@AName", name);
            c.Parameters.AddWithValue("@AStatus", status);
            c.Parameters.AddWithValue("@AStartDate", startdate);
            c.Parameters.AddWithValue("@AEndDate", enddate);
            c.ExecuteNonQuery();
            MessageBox.Show("Added");
            cn.Close();
            display();

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("Update Assignment_TP set AssignmentName=@AName, AssignmentStatus=@AStatus, AssignmentStartDate=@AStartDate, AssignmentEndDate=@AEndDate where AssignmentID=@AID\r\n", cn);
            c.Parameters.AddWithValue("@AID", int.Parse(txtAID.Text));
            c.Parameters.AddWithValue("@AName", txtAName.Text);
            c.Parameters.AddWithValue("@AStatus", comboBox1.SelectedItem.ToString());
            c.Parameters.AddWithValue("@AStartDate", DateTime.Parse(dateTimePicker1.Text).ToString());
            c.Parameters.AddWithValue("@AEndDate", DateTime.Parse(dateTimePicker2.Text).ToString());
            c.ExecuteNonQuery();
            MessageBox.Show("Updated");
            cn.Close();
            display();

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            int a = int.Parse(txtAID.Text);
            SqlCommand c = cn.CreateCommand();
            c.CommandType = CommandType.StoredProcedure;

            c = new SqlCommand("select * from Assignment_TP where AssignmentID=" + a, cn);


            c.Parameters.AddWithValue("@AID", int.Parse(txtAID.Text));

            SqlDataReader dr;
            dr = c.ExecuteReader();
            if (dr.Read())
            {
                txtAName.Text = dr["AssignmentName"].ToString();
                comboBox1.Text = dr["AssignmentStatus"].ToString();
                dateTimePicker2.Text = dr["AssignmentEndDate"].ToString();
                dateTimePicker1.Text = dr["AssignmentStartDate"].ToString();

            }
            else MessageBox.Show("Found None");
            cn.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("Delete from Assignment_TP where AssignmentID=@AID", cn);

            c.Parameters.AddWithValue("@AID", int.Parse(txtAID.Text));

            c.ExecuteNonQuery();
            cn.Close();

            txtAID.Text = "";
            txtAName.Text = "";
            comboBox1.Items.Clear();
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            MessageBox.Show("Deleted successful");
            display();
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            display();
        }

        private void AssignmentManager_Load(object sender, EventArgs e)
        {

        }

        private void AssignmentManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void AssignmentManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
        }
    }
    }

