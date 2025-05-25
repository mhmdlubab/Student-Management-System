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
    public partial class EditStudentForm : Form
    {
        private Student studentToEdit;

        // Constructor
        public EditStudentForm(Student student)
        {
            InitializeComponent();

            studentToEdit = student;

            LoadClasses(); // ✅ Load ComboBox items first

            // Fill form fields
            txtName.Text = studentToEdit.Name;
            txtAge.Text = studentToEdit.Age.ToString();
            cmbClass.SelectedItem = studentToEdit.Class;
            dtpEnrollmentDate.Value = studentToEdit.EnrollmentDate;
        }

        // ✅ Method to load class options into ComboBox
        private void LoadClasses()
        {
            cmbClass.Items.Clear();

            // Example class names — adjust as needed
            cmbClass.Items.Add("Class A");
            cmbClass.Items.Add("Class B");
            cmbClass.Items.Add("Class C");

            // Optionally select the student’s current class if it exists
            if (!string.IsNullOrEmpty(studentToEdit.Class) && cmbClass.Items.Contains(studentToEdit.Class))
            {
                cmbClass.SelectedItem = studentToEdit.Class;
            }
        }

        private bool ValidateName()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter the student's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateAge(out int age)
        {
            if (!int.TryParse(txtAge.Text, out age) || age < 5 || age > 100)
            {
                MessageBox.Show("Please enter a valid age (between 5 and 100).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateClassSelection()
        {
            if (cmbClass.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateName() && ValidateAge(out int age) && ValidateClassSelection())
            {
                // Update the student object
                studentToEdit.Name = txtName.Text;
                studentToEdit.Age = age;
                studentToEdit.Class = cmbClass.Text;
                studentToEdit.EnrollmentDate = dtpEnrollmentDate.Value;

                MessageBox.Show("Student information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
