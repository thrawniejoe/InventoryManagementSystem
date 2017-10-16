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

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Role Check
            if (Properties.Settings.Default.CurrentUserRole == 1)
            {
                tcMainTabController.SelectedIndex = 6;
                tabAdmin.Visibility = Visibility.Visible;
            }
            else
            {
                tabAdmin.Visibility = Visibility.Hidden;
            }

            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table users. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter inventoryDBDataSetusersTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter();
            inventoryDBDataSetusersTableAdapter.Fill(inventoryDBDataSet.Users);
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();
            RefreshUserList();
            // Load data into the table Inventory. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.InventoryTableAdapter inventoryDBDataSetInventoryTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.InventoryTableAdapter();
            inventoryDBDataSetInventoryTableAdapter.Fill(inventoryDBDataSet.Inventory);
            System.Windows.Data.CollectionViewSource inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            inventoryViewSource.View.MoveCurrentToFirst();
            // Load data into the table Users. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter inventoryDBDataSetUsersTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter();
            inventoryDBDataSetUsersTableAdapter.Fill(inventoryDBDataSet.Users);
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                User nu = new User { userID = myid };
                context.Users.Attach(nu); //attaches the user object by the id given to the object above
                context.Users.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshUserList();

        }

        private void btnModifyUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Views.AddUser au = new Views.AddUser();
            au.Owner = this;
            au.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            au.refreshPage += RefreshUserList;
            au.ShowDialog();
        }

        public void RefreshUserList()
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var UserList = (from r in context.Users
                            select r).ToList();
            usersDataGrid.ItemsSource = UserList;
        }
    }
}
