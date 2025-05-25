using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace assignment_c__1._1
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }

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

        private void SaveStudentsToJson()
        {
            try
            {
                // Serialize the students list to JSON
                string json = JsonSerializer.Serialize(databinding.students);

                // Save to the JSON file
                File.WriteAllText("students.json", json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the student data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateName() && ValidateAge(out int age) && ValidateClassSelection())
            {
                // Create new student object
                Student newstudent = new Student
                {
                    Name = txtName.Text,
                    Age = age,
                    Class = cmbClass.Text,
                    EnrollmentDate = dtpEnrollmentDate.Value
                };

                // Add the student to the BindingList
                databinding.students.Add(newstudent);

                // Optionally, save the updated students list to the JSON file
                SaveStudentsToJson();

                MessageBox.Show("Student record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close after successful save
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
