using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment_c__1._1
{
    public partial class DashBoard : Form
    {
        private string userRole;
        public DashBoard(string role)
        {
            InitializeComponent();
            userRole = role;
            ApplyRoleRestrictions();
        }

        private void ApplyRoleRestrictions()
        {
            if (userRole == "Admin")
            {
                // Enable the "Add Student" button for Admins
                btnAddStudent.Enabled = true;
                toolStripButton1.Enabled= true;
            }
            else
            {
                // Disable the "Add Student" button for Teachers
                btnAddStudent.Enabled = false;
                toolStripButton1.Enabled = false;

            }
        }


        // handling clicking the add students button in the toolstrip
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            AddStudentForm addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();
        }

        // handling clicking the view students button in the toolstrip
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ViewStudents viewStudentsForm = new ViewStudents(userRole);

            // Show the ViewStudents form
            viewStudentsForm.Show();
        }

        // handling clicking the search students button in the toolstrip
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Search Students screen coming soon!");
        }

        // handling clicking the exit button in the file tab in the menustrip
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Closes the application
        }

        // handling clicking the add student button in the group box
        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudentForm addStudentForm = new AddStudentForm();
            addStudentForm.ShowDialog();  // Opens as a modal form
        }

        // handling clicking the view students button in the group box
        private void btnViewStudents_Click(object sender, EventArgs e)
        {
            ViewStudents viewStudentsForm = new ViewStudents(userRole);

            // Show the ViewStudents form
            viewStudentsForm.Show();

            
        }

        // handling clicking the search students button in the group box
        private void btnSearchStudents_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Search Students screen coming soon!");
        }
    }
}
