using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using assignment_c__1;

namespace assignment_c__1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // handling clicking the login button in the login form
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string role = comboBoxRole.SelectedItem?.ToString();

            if (role == null)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            if (password != "1234")
            {
                MessageBox.Show("Incorrect password.");
                return;
            }

            if (role == "Admin" && username == "admin")
            {
                MessageBox.Show("Welcome Admin!");
                DashBoard dashboard = new DashBoard(role);
                dashboard.Show();
                this.Hide();
            }
            else if (role == "Teacher" && username == "teacher")
            {
                MessageBox.Show("Welcome Teacher!");
                DashBoard dashboard = new DashBoard(role);
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect username for selected role.");
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
