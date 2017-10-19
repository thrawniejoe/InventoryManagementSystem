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
        //private int userID;
        public string formType;

        public AddItem()
        {
            InitializeComponent();
        }

        public string FormType
        {
            get { return formType; }
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
            assignedToComboBox.DisplayMemberPath = "Name";
            assignedToComboBox.SelectedValuePath = "EmployeeID";

            var categories = (from i in context.Categories
                                select new { category = i.CategoryName}).ToList();
            categoryComboBox.ItemsSource = assignedUser;
            categoryComboBox.DisplayMemberPath = "Category";
            categoryComboBox.SelectedValuePath = "CategoryID";

            var office = (from o in context.OfficeLists
                              select new { id = o.officeID, officeName = o.officeName }).ToList();
            officeIDComboBox.ItemsSource = office;
            officeIDComboBox.DisplayMemberPath = "officeName";
            officeIDComboBox.SelectedValuePath = "officeID";
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Inventory newItem = new Inventory();

            Boolean validated = true;
            //DO VALIDATION CHECK HERE

            if (validated == true)
            {
                int cat = Convert.ToInt16(categoryComboBox.SelectedValue);
                newItem.dateAssigned = Convert.ToDateTime(assignedToComboBox.SelectedValue);
                newItem.CategoryID = cat;
                newItem.dateAssigned = dateAssignedDatePicker.SelectedDate;
                newItem.datePurchased = datePurchasedDatePicker.SelectedDate;
                newItem.dateRecordModified = dateRecordModifiedDatePicker.SelectedDate;



                context.Inventories.Add(newItem);
                context.SaveChanges();
                MessageBox.Show("User " + assignedToComboBox.Text + " Added to the system.");
                RefreshPage();
                this.Close();
            }
        }
    }
}
