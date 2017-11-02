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
        public event Refresh RefreshPage;
        public int userID;

        public string RequestType { get; internal set; }

        public AddUser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var roles = (from r in context.Roles
                         select new { title = r.Title, id = r.roleID }).ToList();
            roleComboBox.ItemsSource = roles;
            roleComboBox.DisplayMemberPath = "title";
            roleComboBox.SelectedValuePath = "id";

            switch (RequestType)
            {
                case "Add":
                    lblTitle.Content = "Add a User";
                    break;

                case "Modify":
                    lblTitle.Content = "Modify a User";
                    var userInfo = (from u in context.Users
                                    where u.userID == userID
                                    select u).FirstOrDefault();

                    User mU = new User();
                    mU = userInfo;
                    if(mU != null)
                    {
                        firstNameTextBox.Text = mU.firstName;
                        lastNameTextBox.Text = mU.lastName;
                        phoneTextBox.Text = mU.phone;
                        roleComboBox.Text = mU.Role.Title;
                        //passwordTextBox.Password = mU.password;
                        emailAddressTextBox.Text = mU.emailAddress;
                    }
                    break;
            }
        }



        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Boolean validated = true;

            //DO VALIDATION CHECK HERE

            switch (RequestType)
            {
                case "Add":
                    User newUser = new User();

                    if (validated == true)
                    {
                        newUser.emailAddress = emailAddressTextBox.Text;
                        newUser.firstName = firstNameTextBox.Text;
                        newUser.lastName = lastNameTextBox.Text;
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
                    break;
                case "Modify":
                    //if (Validate()== true)
                    //{
                        UpdateUserInDB();
                        this.Close();
                        break;
                    //}

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    

        private void UpdateUserInDB()
        {
            //Updates user in database
            using (var db = new InventoryDBEntities())
            {
                var result = db.Users.SingleOrDefault(b => b.userID == userID);
                if (result != null)
                {
                    result.firstName = firstNameTextBox.Text;
                    result.lastName = lastNameTextBox.Text;
                    result.phone = phoneTextBox.Text;
                    result.emailAddress = emailAddressTextBox.Text;
                    result.roleID = Convert.ToInt16(roleComboBox.SelectedValue);

                    // set salt and hashed password, store it to the database
                    byte[] salt = ModelClass.Password.CreateSalt(12);
                    byte[] password = ModelClass.Password.Hash(passwordTextBox.Password, salt);
                    result.hashedPassword = password;
                    result.passwordSalt = salt;
                    db.SaveChanges();
                }
            }
        }


        private Boolean Validate()
        {
            var check = true;
            const int min = 2;
            const int passMin = 6;
            const int max = 30;
            const int exact = 10;
            lblErrorMessage.Content = "";


            //First Name Validation
            if (!Validations.CheckEmptyString(firstNameTextBox.Text))
            {
                lblErrorMessage.Content = "First name cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(firstNameTextBox.Text, min, max))
            {
                lblErrorMessage.Content = "First name must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(firstNameTextBox.Text))
            {
                lblErrorMessage.Content = "First name must be letters only.";
                check = false;
            }

            //Last Name Validation
            if (!Validations.CheckEmptyString(lastNameTextBox.Text))
            {
                lblErrorMessage.Content = "Last name cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(lastNameTextBox.Text, min, max))
            {
                lblErrorMessage.Content = "Last name must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(lastNameTextBox.Text))
            {
                lblErrorMessage.Content = "Last name must be letters only.";
                check = false;
            }

            //Phone Number Validation
            if (!Validations.CheckIfNumeric(phoneTextBox.Text))
            {
                lblErrorMessage.Content = "Phone number must be numbers only.";
                check = false;
            }

            if (!Validations.CheckIfExact(phoneTextBox.Text, exact))
            {
                lblErrorMessage.Content = "Phone number must be " + exact + " in length";
                check = false;
            }

            //Password Validation
            if (!Validations.CheckEmptyString(passwordTextBox.Password))
            {
                lblErrorMessage.Content = "Password cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(passwordTextBox.Password, passMin, max))
            {
                lblErrorMessage.Content = "Password must be between " + passMin + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(passwordTextBox.Password))
            {
                lblErrorMessage.Content = "Password must be letters only.";
                check = false;
            }

            return check;
        }

        public void getID(int myid)
        {
            userID = myid;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
