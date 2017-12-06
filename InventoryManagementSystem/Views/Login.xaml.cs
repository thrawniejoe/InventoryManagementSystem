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
            try
            {

            
            //add user check here
            var password = txtPassword.Password;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            string userName = txtUsername.Text;

            var getRole = (from u in context.Users
                           where u.firstName.Equals(userName)
                           select u.roleID).SingleOrDefault();

            var getID = (from u in context.Users
                           where u.firstName.Equals(userName)
                           select u.userID).SingleOrDefault();

            if (ModelClass.Password.ConfirmPassword(userName, txtPassword.Password))
            {
                Window mainWindow = null;
                mainWindow = new MainScreen();
                Properties.Settings.Default.CurrentUserRole = Convert.ToInt16(getRole); //saves the user role to the applcation settings
                Properties.Settings.Default.CurrentUserID = Convert.ToInt16(getID); //Saves user ID to app settings
                Properties.Settings.Default.Save();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                lblError.Content = "Invalid Username and / or Password.Please try again.";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblError);
                //MessageBox.Show("Invalid Username and/or Password. Please try again.");
            }
            }
            catch
            {
                lblError.Content = "No password found";
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblError);
            }
        }

        private void BtnX_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table Users. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter inventoryDBDataSetUsersTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter();
            inventoryDBDataSetUsersTableAdapter.Fill(inventoryDBDataSet.Users);
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();
        }
    }
}
