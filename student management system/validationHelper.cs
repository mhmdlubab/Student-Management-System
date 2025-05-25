using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment_c__1._1
{
    internal class validationHelper
    {
        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter the student's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool ValidateAge(string ageText, out int age)
        {
            if (!int.TryParse(ageText, out age) || age < 5 || age > 100)
            {
                MessageBox.Show("Please enter a valid age (between 5 and 100).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public static bool ValidateClassSelection(ComboBox cmbClass)
        {
            if (cmbClass.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a class.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}
