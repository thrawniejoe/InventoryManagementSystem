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
using System.Windows.Media.Animation;
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
        public event Refresh RefreshEmployees;
        public int userID;
        public string RequestType { get; internal set; }

        public AddEmployee()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBoxInfo();
            var context = new InventoryManagementSystem.InventoryDBEntities();

            switch (RequestType)
            {
                case "Add":
                    lblTitle.Content = "Add a User";
                    break;

                case "Modify":
                    lblTitle.Content = "Modify a User";
                    BtnAddEmployee.Content = "Modify User";
                    var userInfo = (from u in context.Employees
                                    where u.EmployeeID == userID
                                    select u).FirstOrDefault();

                    Employee mU = new Employee();
                    mU = userInfo;
                    if (mU != null)
                    {
                        nameTextBox.Text = mU.Name;
                        titleTextBox.Text = mU.Title;
                        usernameTextBox.Text = mU.Username;
                        emailAddressTextBox.Text = mU.EmailAddress;
                        managerComboBox.Text = mU.Manager;
                        divisionTextBox.Text = mU.Division;
                        locationComboBox.Text = mU.Location;
                        statusComboBox.Text = mU.Status;
                        phoneNumberTextBox.Text = mU.PhoneNumber;
                    }
                    break;
            }
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


            switch (RequestType)
            {
                case "Add":
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
                        RefreshEmployees();
                        this.Close();
                    }
                    break;
                case "Modify":

                    if (Validate())
                    {
                        UpdateEmployeeInDB();
                        this.Close();
                    }
                    break;

            }



        }

        private void UpdateEmployeeInDB()
        {
            //Updates user in database
            using (var db = new InventoryDBEntities())
            {
                var result = db.Employees.SingleOrDefault(b => b.EmployeeID == userID);
                if (result != null)
                {
                    result.Name = nameTextBox.Text;
                    result.Title = titleTextBox.Text;
                    result.Username = usernameTextBox.Text;
                    result.EmailAddress = emailAddressTextBox.Text;
                    result.Manager = managerComboBox.Text;
                    result.Division = divisionTextBox.Text;
                    result.Location = locationComboBox.Text;
                    result.Status = statusComboBox.Text;
                    result.PhoneNumber = phoneNumberTextBox.Text;
                    db.SaveChanges();
                }
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
            //const int exact = 10;
            lblErrorMessage.Content = "";


            //Name Validation
            if (!Validations.CheckEmptyString(nameTextBox.Text))
            {
                lblErrorMessage.Content = "First name cannot be blank.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            if (!Validations.CheckStringMinMax(nameTextBox.Text, min, max))
            {
                lblErrorMessage.Content = "First name must be between " + min + " and " + max + " in length";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            if (!Validations.CheckIfAlpha(nameTextBox.Text))
            {
                lblErrorMessage.Content = "First name must be letters only.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            //Title Validation
            if (!Validations.CheckEmptyString(titleTextBox.Text))
            {
                lblErrorMessage.Content = "Title cannot be blank.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            //Email Validation
            if (!Validations.CheckEmptyString(emailAddressTextBox.Text))
            {
                lblErrorMessage.Content = "Email cannot be blank.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            //Status Validation
            if (!Validations.CheckEmptyString(statusComboBox.Text))
            {
                lblErrorMessage.Content = "Status cannot be blank.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            //Phone Number Validation
            if (!Validations.CheckIfNumeric(phoneNumberTextBox.Text))
            {
                lblErrorMessage.Content = "Phone number must be numbers only.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblErrorMessage);
                check = false;
            }

            return check;
        }
    }
}
