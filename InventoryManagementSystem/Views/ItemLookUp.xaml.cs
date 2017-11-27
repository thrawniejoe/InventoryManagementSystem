using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ItemLookUp.xaml
    /// </summary>
    public partial class ItemLookUp : Window
    {

        private int currentItemID;
        public delegate void Refresh();
        public event Refresh RefreshPage;
        public string RequestType;
        private string searchType;

        public ItemLookUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table vInventoryList. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.vInventoryListingTableAdapter inventoryDBDataSetvInventoryListTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.vInventoryListingTableAdapter();
            inventoryDBDataSetvInventoryListTableAdapter.Fill(inventoryDBDataSet.vInventoryListing);
            //System.Windows.Data.CollectionViewSource vInventoryListViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vInventoryListViewSource")));
            //vInventoryListViewSource.View.MoveCurrentToFirst();
            // Load data into the table Inventory. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.InventoryTableAdapter inventoryDBDataSetInventoryTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.InventoryTableAdapter();
            inventoryDBDataSetInventoryTableAdapter.Fill(inventoryDBDataSet.Inventory);
            System.Windows.Data.CollectionViewSource inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            inventoryViewSource.View.MoveCurrentToFirst();
            // Load data into the table Documentation. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.DocumentationTableAdapter inventoryDBDataSetDocumentationTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.DocumentationTableAdapter();
            inventoryDBDataSetDocumentationTableAdapter.Fill(inventoryDBDataSet.Documentation);
            System.Windows.Data.CollectionViewSource documentationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("documentationViewSource")));
            documentationViewSource.View.MoveCurrentToFirst();
            LoadDat();

            if (Properties.Settings.Default.DocumentsLocation == "")
            {
                Properties.Settings.Default.DocumentsLocation = System.IO.Path.GetFullPath(".") + "\\documents";
                Properties.Settings.Default.Save();
                MessageBox.Show(Properties.Settings.Default.DocumentsLocation);
            }



            if (RequestType == "ModifyItem")
            {
                InventoryListView();
                LoadDocumentList();
            }
            // Load data into the table vInventoryListing. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.vInventoryListingTableAdapter inventoryDBDataSetvInventoryListingTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.vInventoryListingTableAdapter();
            inventoryDBDataSetvInventoryListingTableAdapter.Fill(inventoryDBDataSet.vInventoryListing);
            System.Windows.Data.CollectionViewSource vInventoryListingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vInventoryListingViewSource")));
            vInventoryListingViewSource.View.MoveCurrentToFirst();
        }


        public void GetID(int myid)
        {
            currentItemID = myid;
        }

        //Loads Combobox data
        private void LoadDat()
        {
            cboEmplyeeList.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();

            //Populate the Employee Combobox
            var EmployeeList_Name = (from e in context.Employees
                                     select new { name = e.Name, id = e.EmployeeID }).ToList();

            cboEmplyeeList.ItemsSource = EmployeeList_Name;
            cboEmplyeeList.SelectedValuePath = "id";
            cboEmplyeeList.DisplayMemberPath = "name";

            //Populate the Office Combobox
            var getOfficeList = (from r in context.OfficeLists
                                 select new { name = r.officeName, id = r.officeID }).ToList();

            cboOfficeList.ItemsSource = getOfficeList;
            cboOfficeList.SelectedValuePath = "id";
            cboOfficeList.DisplayMemberPath = "name";

            officeIDComboBox.ItemsSource = getOfficeList;
            officeIDComboBox.SelectedValuePath = "id";
            officeIDComboBox.DisplayMemberPath = "name";

            //Populate the Tag Combobox
            var TagNumber = (from r in context.Inventories
                             select r.tag).ToList();

            cboTagList.ItemsSource = TagNumber;

            //Populate the Category Combobox
            var getCategory = (from c in context.Categories
                               select new { name = c.CategoryName, id = c.CategoryID }).ToList();

            categoryIDComboBox.ItemsSource = getCategory;
            categoryIDComboBox.SelectedValuePath = "id";
            categoryIDComboBox.DisplayMemberPath = "name";

            //Populate the Location Combobox
            var getLocations = (from l in context.Locations
                                select new { name = l.Location1, id = l.LocationID }).ToList();

            locationIDComboBox.ItemsSource = getLocations;
            locationIDComboBox.SelectedValuePath = "id";
            locationIDComboBox.DisplayMemberPath = "name";

            //Populate the Status Combobox
            var getStatList = (from s in context.StatusLists
                               select new { name = s.Status, id = s.StatusID }).ToList();

            statusIDComboBox.ItemsSource = getStatList;
            statusIDComboBox.SelectedValuePath = "id";
            statusIDComboBox.DisplayMemberPath = "name";

            //Populate the Assigned to Combobox
            var getEmployeeList = (from u in context.Employees
                                   select new { name = u.Name, id = u.EmployeeID }).ToList();

            assignedToComboBox.ItemsSource = getEmployeeList;
            assignedToComboBox.SelectedValuePath = "id";
            assignedToComboBox.DisplayMemberPath = "name";
        }

        //Loads Selected Informatinon
        private void InventoryListView()
        {
            vInventoryListDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();


            var SelectedItem = (from i in context.vInventoryListings
                                where i.itemID == currentItemID  //lists by userid
                                select i).ToList();

            Inventory item = context.Inventories.First(i => i.itemID == currentItemID);
            itemNameTextBox.Text = item.itemName;
            tagTextBox.Text = item.tag;
            serialNumberTextBox.Text = item.serialNumber;
            manufacturerTextBox.Text = item.manufacturer;
            modelIDTextBox.Text = Convert.ToString(item.modelID);
            modelNumberTextBox.Text = item.modelNumber;
            categoryIDComboBox.Text = item.Category.CategoryName;
            locationIDComboBox.Text = item.Location.Location1;
            statusIDComboBox.Text = item.StatusList.Status;
            assignedToComboBox.SelectedValue = item.assignedTo;
            dateAssignedDatePicker.SelectedDate = item.dateAssigned;
            dateRecordModifiedDatePicker.SelectedDate = item.dateRecordModified;
            recordModifiedBy_userIDTextBox.Text = Convert.ToString(item.dateRecordModified);
            datePurchasedDatePicker.SelectedDate = item.datePurchased;
            officeIDComboBox.SelectedValue = item.officeID;

            var InventoryList = (from i in context.vInventoryListings
                                 where i.assignedTo == item.assignedTo  //lists by userid
                                 select i).ToList();

            vInventoryListDataGrid.ItemsSource = InventoryList;
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            Views.AddItem newUser = new Views.AddItem
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            newUser.ShowDialog();
            Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();

            

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Inventory nu = new Inventory { itemID = currentItemID };
                context.Inventories.Attach(nu); //attaches the user object by the id given to the object above
                context.Inventories.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
                RefreshPage();
                this.Close();

                //check the search method and refresh list
                switch (searchType)
                {
                    case "EmployeeSearch":

                        break;
                    case "OfficeSearch":

                        break;
                    case "TagSearch":

                        break;
                }
            }
            
        }


        //Updates the item information in the DB
        private void BtnUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
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
            if (categoryIDComboBox.Text == "")
            {
                errorMessage = errorMessage + " Category is missing. ";
            }
            if (locationIDComboBox.Text == "")
            {
                errorMessage = errorMessage + " Location is missing. ";
            }
            if (statusIDComboBox.Text == "")
            {
                errorMessage = errorMessage + " Status is missing. ";
            }
            if (officeIDComboBox.Text == "")
            {
                errorMessage = errorMessage + " location is missing. ";
            }

            if (errorMessage == "")
            {
                using (var db = new InventoryDBEntities())
                {
                    var item = db.Inventories.SingleOrDefault(b => b.itemID == currentItemID);
                    if (item != null)
                    {
                        item.itemName = itemNameTextBox.Text;
                        item.tag = tagTextBox.Text;
                        item.serialNumber = serialNumberTextBox.Text;
                        item.manufacturer = manufacturerTextBox.Text;
                        //item.modelID = Convert.ToInt16(modelIDTextBox.Text);
                        item.modelNumber = modelNumberTextBox.Text;
                        item.CategoryID = Convert.ToInt16(categoryIDComboBox.SelectedValue);
                        item.LocationID = Convert.ToInt16(locationIDComboBox.SelectedValue);
                        item.StatusID = Convert.ToInt16(statusIDComboBox.SelectedValue);
                        item.assignedTo = Convert.ToInt16(assignedToComboBox.SelectedValue);
                        item.dateAssigned = dateAssignedDatePicker.SelectedDate;
                        item.dateRecordModified = DateTime.Now;
                        item.recordModifiedBy_userID = Properties.Settings.Default.CurrentUserID;
                        item.datePurchased = datePurchasedDatePicker.SelectedDate;
                        item.officeID = Convert.ToInt16(officeIDComboBox.SelectedValue);
                        db.SaveChanges();
                        RefreshPage();
                        this.Close();
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(errorMessage);
            }

        }

        private bool Validate()
        {
            return true;
        }


        private void VInventoryListDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Get Item ID
                var row = (vInventoryListing)vInventoryListDataGrid.SelectedItem;
                currentItemID = row.itemID;

                //Reload Inventory
                InventoryListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Selection, please select a record");
            }

        }

        private void CboEmplyeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var id = Convert.ToInt16(cboEmplyeeList.SelectedValue);

            var getItemsFromEmployee = (from r in context.vInventoryListings
                                        where r.assignedTo == id
                                        select r).ToList();
            vInventoryListDataGrid.ItemsSource = null;
            vInventoryListDataGrid.ItemsSource = getItemsFromEmployee;
        }

        private void CboOfficeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cboOfficeList.SelectedItem != null)
            //{
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var id = Convert.ToInt16(cboOfficeList.SelectedValue);

            var getOfficeList = (from r in context.vInventoryListings
                                 where r.officeID == id
                                 select r).ToList();

            vInventoryListDataGrid.ItemsSource = null;
            vInventoryListDataGrid.ItemsSource = getOfficeList;
            //}

        }

        private void CboTagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            string id = Convert.ToString(cboTagList.SelectedValue);

            var getItemsFromEmployee = (from r in context.vInventoryListings
                                        where r.tag == id
                                        select r).ToList();

            vInventoryListDataGrid.ItemsSource = null;
            vInventoryListDataGrid.ItemsSource = getItemsFromEmployee;
        }

        private void BtnAddDoc_Click(object sender, RoutedEventArgs e)
        {
            string path;
            string filename;

            OpenFileDialog file = new OpenFileDialog
            {
                InitialDirectory = Properties.Settings.Default.DocumentsLocation
            };

            if (file.ShowDialog() == true)
            {
                path = file.FileName;
                filename = file.SafeFileName;
                var mStore = new ModelClass.DocumentStore();
                mStore.AddDocToDB(Properties.Settings.Default.DocumentsLocation, filename, currentItemID);
                MessageBox.Show(path + " ||| " + Properties.Settings.Default.DocumentsLocation);
                File.Copy(path, Properties.Settings.Default.DocumentsLocation);
                LoadDocumentList();
            }
        }

        private void LoadDocumentList()
        {
            documentationDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var DocumentList = (from o in context.Documentations
                                select o).ToList();
            documentationDataGrid.ItemsSource = DocumentList;
        }
    }
}
