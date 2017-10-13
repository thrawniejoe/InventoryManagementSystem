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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    /// 

    public partial class AddUser : Window
    {
        public delegate void Refresh();
        public event Refresh refreshPage;
        public string RequestType;
        private int userID;

        public AddUser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var roles = (from r in context.roles
                         select new { title = r.Title, id = r.roleID }).ToList();
            roleComboBox.ItemsSource = roles;
            roleComboBox.DisplayMemberPath = "title";
            roleComboBox.SelectedValuePath = "id";

            switch (RequestType)
            {
                case "Add":
                    
                    break;

                case "Modify":
                    var userInfo = (from u in context.users
                                    where u.userID == userID
                                    select u).FirstOrDefault();

                    user mU = new user();
                    mU = userInfo;

                    firstNameTextBox.Text = mU.firstName;
                    lastNameTextBox.Text = mU.lastName;
                    phoneTextBox.Text = mU.phone;
                    roleComboBox.Text = mU.role.Title;
                    passwordTextBox.Text = mU.password;
                    emailAddressTextBox.Text = mU.emailAddress;
                    break;
            }


        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            user newUser = new user();

            Boolean validated = true;
            //DO VALIDATION CHECK HERE

            if(validated == true)
            {
                newUser.emailAddress = emailAddressTextBox.Text;
                newUser.firstName = firstNameTextBox.Text;
                newUser.lastName = lastNameTextBox.Text;
                newUser.password = passwordTextBox.Text;
                newUser.phone = phoneTextBox.Text;
                newUser.roleID = Convert.ToInt16(roleComboBox.SelectedValue);
                newUser.title = "Not Availible";
                context.users.Add(newUser);
                context.SaveChanges();
                MessageBox.Show("User " + firstNameTextBox.Text + " " + lastNameTextBox.Text +" Added to the system.");
                refreshPage();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
