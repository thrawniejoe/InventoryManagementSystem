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
                tabAdmin.Visibility = Visibility.Visible;
            }
            else
            {
                tabAdmin.Visibility = Visibility.Hidden;
            }
            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table users. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.usersTableAdapter inventoryDBDataSetusersTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.usersTableAdapter();
            inventoryDBDataSetusersTableAdapter.Fill(inventoryDBDataSet.users);
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnModifyUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
