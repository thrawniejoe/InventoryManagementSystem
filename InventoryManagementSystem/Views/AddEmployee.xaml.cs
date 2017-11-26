using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public delegate void Refresh();
        public event Refresh RefreshEmployeeList;

        public AddEmployee()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBoxInfo();
        }

        private void LoadComboBoxInfo()
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();


            //Location Cbo
            var getLocations = (from c in context.Locations
                               select new { name = c.Location1 }).ToList();

            locationComboBox.ItemsSource = getLocations;
            locationComboBox.SelectedValuePath = "name";
            locationComboBox.DisplayMemberPath = "name";

            //Status Cbo
            var getStatus = (from c in context.StatusLists
                                select new { name = c.Status }).ToList();

            statusComboBox.ItemsSource = getStatus;
            statusComboBox.SelectedValuePath = "name";
            statusComboBox.DisplayMemberPath = "name";

            //Manager Cbo
            var getManagers = (from c in context.Employees
                             where c.Title == "Manager"
                             select new { name = c.Name }).ToList();

            managerComboBox.ItemsSource = getManagers;
            managerComboBox.SelectedValuePath = "name";
            managerComboBox.DisplayMemberPath = "name";
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = new Employee();
            var context = new InventoryManagementSystem.InventoryDBEntities();
            if (Validate())
            {
                newEmployee.Name = nameTextBox.Text;
                newEmployee.Title = titleTextBox.Text;
                newEmployee.Username = usernameTextBox.Text;
                newEmployee.EmailAddress = emailAddressTextBox.Text;
                newEmployee.Manager = managerComboBox.Text;
                newEmployee.Division = divisionTextBox.Text;
                newEmployee.Location = locationComboBox.Text;
                newEmployee.Status = statusComboBox.Text;
                newEmployee.PhoneNumber = phoneNumberTextBox.Text;
                context.Employees.Add(newEmployee);
                context.SaveChanges();
                MessageBox.Show("User " + nameTextBox.Text + " Added to the system.");
                RefreshEmployeeList();
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Boolean Validate()
        {
            var check = true;
            const int min = 2;
            //const int passMin = 6;
            const int max = 30;
            const int exact = 10;
            lblErrorMessage.Content = "";


            //Name Validation
            if (!Validations.CheckEmptyString(nameTextBox.Text))
            {
                lblErrorMessage.Content = "First name cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(nameTextBox.Text, min, max))
            {
                lblErrorMessage.Content = "First name must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(nameTextBox.Text))
            {
                lblErrorMessage.Content = "First name must be letters only.";
                check = false;
            }

            //Title Validation
            if (!Validations.CheckEmptyString(titleTextBox.Text))
            {
                lblErrorMessage.Content = "Title cannot be blank.";
                check = false;
            }

            //Email Validation
            if (!Validations.CheckEmptyString(emailAddressTextBox.Text))
            {
                lblErrorMessage.Content = "Email cannot be blank.";
                check = false;
            }

            //Status Validation
            if (!Validations.CheckEmptyString(statusComboBox.Text))
            {
                lblErrorMessage.Content = "Status cannot be blank.";
                check = false;
            }

            //Phone Number Validation
            if (!Validations.CheckIfNumeric(phoneNumberTextBox.Text))
            {
                lblErrorMessage.Content = "Phone number must be numbers only.";
                check = false;
            }

            if (!Validations.CheckIfExact(phoneNumberTextBox.Text, exact))
            {
                lblErrorMessage.Content = "Phone number must be " + exact + " in length";
                check = false;
            }
            return check;
        }
    }
}
