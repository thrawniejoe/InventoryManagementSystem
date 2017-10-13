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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //add user check here
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var context = new InventoryManagementSystem.InventoryDBEntities();

            var getPass = (from user in context.users
                          where user.emailAddress == username
                          select user.password).FirstOrDefault();

            var getRole = (from u in context.users
                           where u.emailAddress.Equals(username)
                           select u.roleID).SingleOrDefault();

            if (password == getPass)
            {
                Window mainWindow = null;
                switch (getRole)
                {
                    case 1:  //Admin
                        mainWindow = new MainScreen();
                        break;
                    case 2:  //Manager
                        //mainWindow = new ManagerMain();
                        break;
                    case 3:  //User
                        //mainWindow = new MainWindow();
                        break;
                }

                Properties.Settings.Default.CurrentUserRole = getRole; //saves the user role to the applcation settings file
                Properties.Settings.Default.Save();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password. Please try again.");
            }
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


    }
}
