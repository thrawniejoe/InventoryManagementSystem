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

            LoadDat();
        }

        //Loads Combobox data
        private void LoadDat()
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();

            //Populate the Office Combobox
            var Office = (from r in context.OfficeLists
                          select r.officeName).ToList();

            officeIDComboBox.ItemsSource = Office;


            //Populate the Category Combobox
            var getCategory = (from c in context.Categories
                               select new { name = c.CategoryName, id = c.CategoryID }).ToList();

            categoryComboBox.ItemsSource = getCategory;
            categoryComboBox.SelectedValuePath = "id";
            categoryComboBox.DisplayMemberPath = "name";

            //Populate the Location Combobox
            var getLocations = (from l in context.Locations
                                select new { name = l.Location1, id = l.LocationID }).ToList();

            locationComboBox.ItemsSource = getLocations;
            locationComboBox.SelectedValuePath = "id";
            locationComboBox.DisplayMemberPath = "name";

            //Populate the Status Combobox
            var getStatList = (from s in context.StatusLists
                               select new { name = s.Status, id = s.StatusID }).ToList();

            statusComboBox.ItemsSource = getStatList;
            statusComboBox.SelectedValuePath = "id";
            statusComboBox.DisplayMemberPath = "name";

            //Populate the Assigned to Combobox
            var getEmployeeList = (from u in context.Employees
                                   select new { name = u.Name, id = u.EmployeeID }).ToList();

            assignedToComboBox.ItemsSource = getEmployeeList;
            assignedToComboBox.SelectedValuePath = "id";
            assignedToComboBox.DisplayMemberPath = "name";

            //Populate the Office Combobox
            var getOfficeList = (from r in context.OfficeLists
                                 select new { name = r.officeName, id = r.officeID }).ToList();

            officeIDComboBox.ItemsSource = getOfficeList;
            officeIDComboBox.SelectedValuePath = "id";
            officeIDComboBox.DisplayMemberPath = "name";
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Inventory newItem = new Inventory();
            string errorMessage = "";

            //validation
            if (tagTextBox.Text == "")
            {
                errorMessage = "Missing item tag. ";
            }
            if (assignedToComboBox.Text == "")
            {
                errorMessage = errorMessage + " Missing Checked Out To. ";
            }
            if (categoryComboBox.Text == "")
            {
                errorMessage = errorMessage + " Category is missing. ";
            }
            if (locationComboBox.Text == "")
            {
                errorMessage = errorMessage + " Location is missing. ";
            }
            if (statusComboBox.Text == "")
            {
                errorMessage = errorMessage + " Status is missing. ";
            }
            if (officeIDComboBox.Text == "")
            {
                errorMessage = errorMessage + " location is missing. ";
            }

            if (errorMessage == "")
            {
                int cat = Convert.ToInt16(categoryComboBox.SelectedValue);
                newItem.CategoryID = cat;
                newItem.dateAssigned = dateAssignedDatePicker.SelectedDate;
                newItem.datePurchased = datePurchasedDatePicker.SelectedDate;
                newItem.dateRecordModified = DateTime.Today;
                newItem.assignedTo = Convert.ToInt16(assignedToComboBox.SelectedValue);
                newItem.tag = tagTextBox.Text;
                newItem.StatusID = Convert.ToInt16(statusComboBox.SelectedValue);
                newItem.serialNumber = serialNumberTextBox.Text;
                newItem.officeID = Convert.ToInt16(officeIDComboBox.SelectedValue);
                newItem.manufacturer = manufacturerTextBox.Text;
                newItem.recordModifiedBy_userID = Properties.Settings.Default.CurrentUserID;
                newItem.itemName = itemNameTextBox.Text;
                //newItem.modelID = 0;
                newItem.LocationID = Convert.ToInt16(locationComboBox.SelectedValue);
                context.Inventories.Add(newItem);
                context.SaveChanges();
                MessageBox.Show("User " + assignedToComboBox.Text + " Added to the system.");
                RefreshPage();
                this.Close();
            }
            else
            {
                lblError.Content = errorMessage;
                Storyboard sb = Resources["sbHideAnimation"] as Storyboard;
                sb.Begin(lblError);
            }
        }
    }
}
