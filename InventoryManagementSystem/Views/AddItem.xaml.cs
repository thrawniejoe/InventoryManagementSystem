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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {

        public delegate void Refresh();
        public event Refresh RefreshPage;
        public string RequestType;
        private int userID;

        public AddItem()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table Inventory. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.InventoryTableAdapter inventoryDBDataSetInventoryTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.InventoryTableAdapter();
            inventoryDBDataSetInventoryTableAdapter.Fill(inventoryDBDataSet.Inventory);
            System.Windows.Data.CollectionViewSource inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            inventoryViewSource.View.MoveCurrentToFirst();

            var context = new InventoryManagementSystem.InventoryDBEntities();
            var assignedUser = (from emp in context.Employees
                         select new { name = emp.Name, id = emp.EmployeeID }).ToList();
            assignedToComboBox.ItemsSource = assignedUser;
            assignedToComboBox.DisplayMemberPath = "name";
            assignedToComboBox.SelectedValuePath = "id";

            var categories = (from i in context.Inventories
                                select new { category = i.category}).ToList();
            categoryComboBox.ItemsSource = assignedUser;
            categoryComboBox.DisplayMemberPath = "category";
            categoryComboBox.SelectedValuePath = "category";

            var office = (from o in context.OfficeLists
                              select new { id = o.officeID, officeName = o.officeName }).ToList();
            officeIDComboBox.ItemsSource = office;
            officeIDComboBox.DisplayMemberPath = "officeName";
            officeIDComboBox.SelectedValuePath = "id";
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Inventory newItem = new Inventory();

            Boolean validated = true;
            //DO VALIDATION CHECK HERE

            if (validated == true)
            {
                newItem.assignedTo = Convert.ToInt16(assignedToComboBox.SelectedValue);
                newItem.category = categoryComboBox.Text;

                //newUser.firstName = firstNameTextBox.Text;
                //newUser.lastName = lastNameTextBox.Text;
                //newUser.password = passwordTextBox.Text;
                //newUser.phone = phoneTextBox.Text;
                //newUser.roleID = Convert.ToInt16(roleComboBox.SelectedValue);
                //newUser.title = "Not Availible";


                context.Inventories.Add(newItem);
                context.SaveChanges();
                MessageBox.Show("User " + assignedToComboBox.Text + " Added to the system.");
                RefreshPage();
                this.Close();
            }
        }
    }
}
