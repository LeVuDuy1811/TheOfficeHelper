using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TheOfficeHelper
{
    public partial class MainFrom : Form
    {
        public bool isExit = true;
        public event EventHandler Logout;

        SqlConnection cn = new SqlConnection(@"Data Source=LYNOLEE;Initial Catalog=EmployeeDetail;Integrated Security=True");
        public MainFrom()
        {
            InitializeComponent();
        }
        #region Method

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        bool checkCanDo()
        {
            return !String.IsNullOrEmpty(txtName.Text) || (!String.IsNullOrEmpty(txtID.Text)) || (!String.IsNullOrEmpty(txtSalary.Text))
                || !String.IsNullOrEmpty(txtAddress.Text) || !String.IsNullOrEmpty(comboBox1.SelectedItem.ToString());
        }

            private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.checkCanDo() && Regex.IsMatch(txtSalary.Text, @"^\d+$") && Regex.IsMatch(txtID.Text, @"^\d+$"))
            {
                cn.Open();
                int i = int.Parse(txtID.Text);
                string name = txtName.Text;
                double salary = double.Parse(txtSalary.Text);
                string address = txtAddress.Text;
                string gender = comboBox1.SelectedItem.ToString();
                string DOB = DateTime.Parse(dateTimePicker1.Text).ToString();

                string mail = txtEmail.Text;
                string phone = txtPhone.Text;
                SqlCommand c = new SqlCommand("Insert into EmployeeDetail_TP(ID, Name, Salary, Adresses, Gender, Email, [Day of Birth], Phone) values(@EID, @EName, @ESalary, @EAdresses, @EGender, @EEmail, @EDoB, @EPhone)\r\n", cn);
                c.Parameters.AddWithValue("@EID", i);
                c.Parameters.AddWithValue("@EName", name);
                c.Parameters.AddWithValue("@ESalary", salary);
                c.Parameters.AddWithValue("@EAdresses", address);
                c.Parameters.AddWithValue("@EGender", gender);
                c.Parameters.AddWithValue("@EDoB", DOB);
                c.Parameters.AddWithValue("@EEmail", mail);
                c.Parameters.AddWithValue("@EPhone", phone);
                c.ExecuteNonQuery();
                MessageBox.Show("Added");
                cn.Close();
                display();

            }
        }

       
        void display()
        {
            cn.Open();
            SqlCommand c = new SqlCommand("select * from EmployeeDetail_TP\r\n", cn);
            c.ExecuteNonQuery();
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            cn.Close();

        }



        #endregion




        #region Event

        private void MainFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }

        }

        private void MainFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        #endregion


        private void MainFrom_Load(object sender, EventArgs e)
        {
            display();
        }
        
        private void btnDisplay_Click_1(object sender, EventArgs e)
        {
            display();
            MessageBox.Show("Displayed");
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("Update EmployeeDetail_TP set Name = @EName, Salary= @ESalary, Adresses=@EAdresses, Gender=@EGender, Email=@EEmail, Phone=@EPhone, [Day of Birth]=@EDoB where ID = @EID\r\n", cn);
            c.Parameters.AddWithValue("@EID", int.Parse(txtID.Text));
            c.Parameters.AddWithValue("@EName", txtName.Text);
            c.Parameters.AddWithValue("@ESalary", double.Parse(txtSalary.Text));
            c.Parameters.AddWithValue("@EAdresses", txtAddress.Text);
            c.Parameters.AddWithValue("@EGender", comboBox1.SelectedItem.ToString());
            c.Parameters.AddWithValue("@EDoB", DateTime.Parse(dateTimePicker1.Text).ToString());
            c.Parameters.AddWithValue("@EEmail", txtEmail.Text);
            c.Parameters.AddWithValue("@EPhone", txtPhone.Text);
            c.ExecuteNonQuery();
            MessageBox.Show("Updated");
            cn.Close();
            display();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            int i = int.Parse(txtID.Text);
            SqlCommand c = cn.CreateCommand();
            c.CommandType = CommandType.StoredProcedure;

            c = new SqlCommand("select * from EmployeeDetail_TP where ID=" + i, cn);

            c.Parameters.AddWithValue("@EID", int.Parse(txtID.Text));

            SqlDataReader dr;
            dr = c.ExecuteReader();
            if (dr.Read())
            {
                txtName.Text = dr["Name"].ToString();
                txtSalary.Text = dr["Salary"].ToString();
                txtAddress.Text = dr["Adresses"].ToString();
                comboBox1.Text = dr["Gender"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                dateTimePicker1.Text = dr["Day of Birth"].ToString();


            }
            else MessageBox.Show("Found None");
            cn.Close();

        }

        private void btnDel_Click_1(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand c = new SqlCommand("Delete from EmployeeDetail_TP where ID=@EID", cn);

            c.Parameters.AddWithValue("@EID", int.Parse(txtID.Text));

            c.ExecuteNonQuery();
            cn.Close();
            
            txtID.Text = "";
            txtName.Text = "";
            txtSalary.Text = "";
            txtAddress.Text = "";
            MessageBox.Show("Deleted successful");
            display();
    }

        private void MainFrom_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                if (MessageBox.Show("Do you want exit?", "warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    e.Cancel = true;
            }

        }

        private void MainFrom_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            if (isExit)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
        }
    }
}
