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

            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table Employees. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.EmployeesTableAdapter inventoryDBDataSetEmployeesTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.EmployeesTableAdapter();
            inventoryDBDataSetEmployeesTableAdapter.Fill(inventoryDBDataSet.Employees);
            System.Windows.Data.CollectionViewSource employeesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeesViewSource")));
            employeesViewSource.View.MoveCurrentToFirst();
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee newUser = new Employee();

            if (validated == true)
            {
                newUser.emailAddress = emailAddressTextBox.Text;
                newUser.firstName = firstNameTextBox.Text;
                newUser.lastName = lastNameTextBox.Text;

                // set salt and hashed password, store it to the database
                byte[] salt = ModelClass.Password.CreateSalt(12);
                byte[] password = ModelClass.Password.Hash(passwordTextBox.Password, salt);
                newUser.hashedPassword = password;
                newUser.passwordSalt = salt;
                newUser.password = passwordTextBox.Password;
                newUser.phone = phoneTextBox.Text;
                newUser.roleID = Convert.ToInt16(roleComboBox.SelectedValue);
                newUser.title = "Not Availible";
                context.Users.Add(newUser);
                context.SaveChanges();
                MessageBox.Show("User " + firstNameTextBox.Text + " " + lastNameTextBox.Text + " Added to the system.");
                RefreshPage();
                this.Close();
            }





            RefreshEmployeeList();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
