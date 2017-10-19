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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            //add user check here
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var context = new InventoryManagementSystem.InventoryDBEntities();
            string userName = txtUsername.Text;

            var uCheck = (from u in context.Users
                          where u.firstName.Equals(userName)
                          select u.password).SingleOrDefault();

            var getRole = (from u in context.Users
                           where u.firstName.Equals(userName)
                           select u.roleID).SingleOrDefault();

            if (ModelClass.Password.ConfirmPassword(userName, txtPassword.Password))
            {
                Window mainWindow = null;
                switch (getRole)
                {
                    case 1:
                        mainWindow = new MainScreen();
                        break;
                    case 2:
                        mainWindow = new MainScreen();
                        break;
                    case 3:
                        mainWindow = new MainScreen();
                        break;
                }
                Properties.Settings.Default.CurrentUserRole = Convert.ToInt16(getRole); //saves the user role to the applcation settings file
                Properties.Settings.Default.Save();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password. Please try again.");
            }
        }

        private void BtnX_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
