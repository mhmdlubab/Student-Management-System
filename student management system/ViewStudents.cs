using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;


namespace assignment_c__1._1
{
    public partial class ViewStudents : Form
    {
        private BindingList<Student> students => databinding.students;

        private string userRole;

        public ViewStudents(string role)
        {
            InitializeComponent();
            userRole = role;
            LoadStudents(); // Load students when the form opens
            dgvStudents.DataSource = null;
            dgvStudents.DataSource = students;
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            ApplyRoleRestrictions();
        }

        private void ApplyRoleRestrictions()
        {
            // Check the role of the current user
            if (userRole == "Admin")
            {
                // Admins can use all controls (Enable buttons)
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                
            }
            else
            {
                // Teachers cannot use certain controls (Disable buttons)
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                
            }
        }


        private void LoadStudents()
        {
            if (databinding.students.Count > 0) return; // Prevent duplicate entries


            // Sample students
            databinding.students.Add(new Student { Name = "John Doe", Age = 18, Class = "Class A", EnrollmentDate = DateTime.Now.AddYears(-2) });
            databinding.students.Add(new Student { Name = "Jane Smith", Age = 20, Class = "Class B", EnrollmentDate = DateTime.Now.AddYears(-1) });
            databinding.students.Add(new Student { Name = "Sam Johnson", Age = 19, Class = "Class A", EnrollmentDate = DateTime.Now.AddMonths(-6) });
            databinding.students.Add(new Student { Name = "Emily Davis", Age = 22, Class = "Class C", EnrollmentDate = DateTime.Now.AddYears(-3) });
            databinding.students.Add(new Student { Name = "Michael Brown", Age = 21, Class = "Class B", EnrollmentDate = DateTime.Now.AddMonths(-8) });
            databinding.students.Add(new Student { Name = "Sarah Wilson", Age = 17, Class = "Class A", EnrollmentDate = DateTime.Now.AddYears(-4) });
            databinding.students.Add(new Student { Name = "David Lee", Age = 23, Class = "Class C", EnrollmentDate = DateTime.Now.AddYears(-5) });
            databinding.students.Add(new Student { Name = "Sophia Clark", Age = 20, Class = "Class B", EnrollmentDate = DateTime.Now.AddMonths(-2) });
            databinding.students.Add(new Student { Name = "Daniel Martinez", Age = 19, Class = "Class A", EnrollmentDate = DateTime.Now.AddMonths(-1) });
            databinding.students.Add(new Student { Name = "Olivia Thomas", Age = 22, Class = "Class C", EnrollmentDate = DateTime.Now.AddYears(-2) });
            databinding.students.Add(new Student { Name = "James Harris", Age = 21, Class = "Class A", EnrollmentDate = DateTime.Now.AddYears(-1) });
            databinding.students.Add(new Student { Name = "Chloe Jackson", Age = 18, Class = "Class B", EnrollmentDate = DateTime.Now.AddYears(-3) });
            databinding.students.Add(new Student { Name = "Benjamin White", Age = 24, Class = "Class C", EnrollmentDate = DateTime.Now.AddMonths(-5) });
            databinding.students.Add(new Student { Name = "Mia Walker", Age = 19, Class = "Class A", EnrollmentDate = DateTime.Now.AddYears(-2) });
            databinding.students.Add(new Student { Name = "Lucas Hall", Age = 23, Class = "Class B", EnrollmentDate = DateTime.Now.AddYears(-4) });
            databinding.students.Add(new Student { Name = "Grace Allen", Age = 20, Class = "Class C", EnrollmentDate = DateTime.Now.AddMonths(-3) });
            databinding.students.Add(new Student { Name = "Ethan Young", Age = 21, Class = "Class A", EnrollmentDate = DateTime.Now.AddMonths(-7) });
            databinding.students.Add(new Student { Name = "Charlotte King", Age = 22, Class = "Class B", EnrollmentDate = DateTime.Now.AddYears(-2) });
            databinding.students.Add(new Student { Name = "Aiden Scott", Age = 19, Class = "Class C", EnrollmentDate = DateTime.Now.AddYears(-3) });
            databinding.students.Add(new Student { Name = "Amelia Wright", Age = 18, Class = "Class A", EnrollmentDate = DateTime.Now.AddMonths(-4) });

            // Bind the list to DataGridView
            dgvStudents.DataSource = null;
            dgvStudents.DataSource = databinding.students;
            dgvStudents.ClearSelection(); // Must be after DataSource is set



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearchName.Text.ToLower().Trim();
            string selectedClass = cmbSearchClass.SelectedItem?.ToString();

            var filteredStudents = databinding.students.Where(s =>
                (string.IsNullOrEmpty(searchName) || s.Name.ToLower().Contains(searchName)) &&
                (string.IsNullOrEmpty(selectedClass) || selectedClass == "All Classes" || s.Class == selectedClass)
            ).ToList();

            dgvStudents.DataSource = null;
            dgvStudents.DataSource = new BindingList<Student>(filteredStudents);

            if (filteredStudents.Count == 0)
            {
                MessageBox.Show("No students found matching the search criteria.");
                dgvStudents.DataSource = databinding.students; // Reset to show all
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

            // Optionally, reset the search fields
            txtSearchName.Clear();

            // Reset the DataGridView to show all students again
            dgvStudents.DataSource = null; // Clear binding first
            dgvStudents.DataSource = databinding.students; // Re-bind the shared student list
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvStudents.SelectedRows[0].Index;
                Student selectedStudent = databinding.students[selectedIndex];

                // Open the Edit form
                EditStudentForm editForm = new EditStudentForm(selectedStudent);
                editForm.ShowDialog();

                // Refresh the grid to show updated data
                dgvStudents.DataSource = null;
                dgvStudents.DataSource = databinding.students;
            }
            else
            {
                MessageBox.Show("Please select a student. Click on arrow to left of record.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvStudents.SelectedRows[0].Index;

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this student?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    // Remove from the list
                    databinding.students.RemoveAt(selectedIndex);

                    // Refresh the DataGridView
                    dgvStudents.DataSource = null;
                    dgvStudents.DataSource = databinding.students;

                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete. Click on arrow to left of record.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ViewStudentsForm_Shown(object sender, EventArgs e)
        {
            dgvStudents.ClearSelection();
            dgvStudents.CurrentCell = null;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("STUDENT REPORT");
            reportBuilder.AppendLine("====================");

            foreach (var student in databinding.students)
            {
                reportBuilder.AppendLine($"Name: {student.Name}, Age: {student.Age}, Class: {student.Class}");
            }

            txtReport.Text = reportBuilder.ToString(); // Show in RichTextBox
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Draw the report content from the RichTextBox onto the print page
            e.Graphics.DrawString(
                txtReport.Text,
                new Font("Arial", 12),
                Brushes.Black,
                new RectangleF(100, 100, 700, 1000)
            );
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument1; // This must be added via Designer
            previewDialog.ShowDialog();
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            // Open SaveFileDialog to choose where to save the report
            saveFileDialog1.Filter = "Text Files|*.txt";  // Filters to show only text files
            saveFileDialog1.Title = "Save the Student Report";  // Title of the Save File dialog

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the file path from the dialog and save the report to that path
                string filePath = saveFileDialog1.FileName;

                try
                {
                    // Ensure that the path is valid before trying to write
                    if (string.IsNullOrEmpty(filePath))
                    {
                        MessageBox.Show("File path is invalid. Please select a valid location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Write the report content from the RichTextBox to the selected file
                    File.WriteAllText(filePath, txtReport.Text);

                    // Notify user the file has been saved
                    MessageBox.Show("Report saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // If there is an error, show an error message
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
