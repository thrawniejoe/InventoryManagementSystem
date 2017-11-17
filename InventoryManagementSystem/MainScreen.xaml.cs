using Microsoft.Win32;
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
        public delegate void getItemID(int uid);

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
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();
            RefreshUserList();
            // Load data into the table Users. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter inventoryDBDataSetUsersTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.UsersTableAdapter();
            inventoryDBDataSetUsersTableAdapter.Fill(inventoryDBDataSet.Users);
            // Load data into the table Employees. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.EmployeesTableAdapter inventoryDBDataSetEmployeesTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.EmployeesTableAdapter();
            inventoryDBDataSetEmployeesTableAdapter.Fill(inventoryDBDataSet.Employees);
            System.Windows.Data.CollectionViewSource employeesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeesViewSource")));
            employeesViewSource.View.MoveCurrentToFirst();

            RefreshFilterList();
            RefreshInventory();
            // Load data into the table OfficeList. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.OfficeListTableAdapter inventoryDBDataSetOfficeListTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.OfficeListTableAdapter();
            inventoryDBDataSetOfficeListTableAdapter.Fill(inventoryDBDataSet.OfficeList);
            System.Windows.Data.CollectionViewSource officeListViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("officeListViewSource")));
            officeListViewSource.View.MoveCurrentToFirst();
            // Load data into the table StatusList. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.StatusListTableAdapter inventoryDBDataSetStatusListTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.StatusListTableAdapter();
            inventoryDBDataSetStatusListTableAdapter.Fill(inventoryDBDataSet.StatusList);
            System.Windows.Data.CollectionViewSource statusListViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("statusListViewSource")));
            statusListViewSource.View.MoveCurrentToFirst();
            // Load data into the table Locations. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.LocationsTableAdapter inventoryDBDataSetLocationsTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.LocationsTableAdapter();
            inventoryDBDataSetLocationsTableAdapter.Fill(inventoryDBDataSet.Locations);
            System.Windows.Data.CollectionViewSource locationsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("locationsViewSource")));
            locationsViewSource.View.MoveCurrentToFirst();
            // Load data into the table Categories. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.CategoriesTableAdapter inventoryDBDataSetCategoriesTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.CategoriesTableAdapter();
            inventoryDBDataSetCategoriesTableAdapter.Fill(inventoryDBDataSet.Categories);
            System.Windows.Data.CollectionViewSource categoriesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("categoriesViewSource")));
            categoriesViewSource.View.MoveCurrentToFirst();
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
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

        private void BtnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            Views.AddUser modifyUser = new Views.AddUser
            {

                userID = Convert.ToInt16(myid),
                RequestType = "Modify",
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            modifyUser.RefreshPage += RefreshUserList;
            modifyUser.ShowDialog();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Views.AddUser addUser = new Views.AddUser
            {
                RequestType = "Add",
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            addUser.RefreshPage += RefreshUserList;
            addUser.ShowDialog();
        }
        //--------------------------------------//
        //********** Refresh Group *************//
        //--------------------------------------//

        public void RefreshUserList()
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var UserList = (from r in context.Users
                            select r).ToList();
            usersDataGrid.ItemsSource = UserList;
        }

        public void RefreshFilterList()
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var FilterList = (from r in context.Categories
                              select r.CategoryName).ToList();

            cboFilterList.ItemsSource = FilterList;
        }

        public void RefreshInventory()
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            string value = Convert.ToString(cboFilterList.SelectedValue);
            //MessageBox.Show(value);
            if (cboFilterList.Text != "")
            {
                var InventoryList = (from i in context.vInventoryLists
                                     where i.CategoryName == value
                                     select i).ToList();
                vInventoryListDataGrid.ItemsSource = InventoryList;
            }
            else
            {
                var InventoryList = (from i in context.vInventoryLists
                                     select i).ToList();
                vInventoryListDataGrid.ItemsSource = InventoryList;
            }
        }

        //--------------------------------------//
        //**********End Refresh Group **********//
        //--------------------------------------//
        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            Views.AddItem additem = new Views.AddItem
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            additem.RefreshPage += RefreshUserList;
            additem.ShowDialog();
        }

        private void BtnModitfyItem_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);
            Views.ItemLookUp lookUp = new Views.ItemLookUp();
            getItemID del = new getItemID(lookUp.GetID);
            //MessageBox.Show(Convert.ToString(myid));
            if (myid != 0)
            {
                del(myid); //sets delagate value
                lookUp.RequestType = "ModifyItem";
            }
            else
            {
                lookUp.RequestType = "LookUpItem";
            }
            lookUp.Owner = this;
            lookUp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            lookUp.RefreshPage += RefreshInventory;
            lookUp.ShowDialog();
            //newUser.RefreshPage += RefreshUserList;          
        }

        private void BtnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cboFilterList.Text = "";
            RefreshInventory();
        }

        private void CboFilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshInventory();
        }

        private void BtnRemoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Inventory nu = new Inventory { itemID = myid };
                context.Inventories.Attach(nu); //attaches the user object by the id given to the object above
                context.Inventories.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshInventory();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void BtnLookUpItem_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);
            Views.ItemLookUp lookUp = new Views.ItemLookUp();
            getItemID del = new getItemID(lookUp.GetID);
            lookUp.RequestType = "LookUpItem";
            lookUp.Owner = this;
            lookUp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            lookUp.RefreshPage += RefreshInventory;
            lookUp.ShowDialog();
        }

        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        //****************************//
        //   Administration - Office  //
        //****************************//
        private void BtnAddOffice_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            OfficeList newOffice = new OfficeList();
            Boolean validateOffice = true;
            //DO VALIDATION CHECK HERE            
            if (validateOffice == true)
            {
                newOffice.officeName = officeNameTextBox.Text;
                newOffice.officeFloor = Convert.ToInt16(officeFloorTextBox.Text);
                context.OfficeLists.Add(newOffice);
                context.SaveChanges();
                RefreshOfficeList();
            }
        }

        private void RefreshOfficeList()
        {
            officeListDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var OfficeList = (from o in context.OfficeLists
                            select o).ToList();
            officeListDataGrid.ItemsSource = OfficeList;
        }

        private void BtnDeleteOffice_CLick(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this office?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                OfficeList nu = new OfficeList { officeID = myid };
                context.OfficeLists.Attach(nu); //attaches the office object by the id given to the object above
                context.OfficeLists.Remove(nu); //Adds the change to Deletes the office from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshOfficeList();
        }
        //****************************//
        //      END ADMIN OFFICE      //
        //****************************//


        //****************************//
        //   Administration - Status  //
        //****************************//
        private void BtnAddStatus_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            StatusList newStatus = new StatusList();
            Boolean validateStatus = true;
            //DO VALIDATION CHECK HERE            
            if (validateStatus == true)
            {
                newStatus.Status = statusTextBox.Text;
                context.StatusLists.Add(newStatus);
                context.SaveChanges();
                RefreshStatusList();
            }
        }

        private void RefreshStatusList()
        {
            statusListDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var StatusList = (from o in context.StatusLists
                              select o).ToList();
            statusListDataGrid.ItemsSource = StatusList;
        }

        private void BtnDeleteStatus_CLick(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Status?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                StatusList nu = new StatusList { StatusID = myid };
                context.StatusLists.Attach(nu); //attaches the Status object by the id given to the object above
                context.StatusLists.Remove(nu); //Adds the change to Deletes the office from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshStatusList();
        }
        //****************************//
        //      END ADMIN STATUS      //
        //****************************//

        //****************************//
        // Administration - Locations //
        //****************************//
        private void BtnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Location newLocation = new Location();
            Boolean validateStatus = true;
            //DO VALIDATION CHECK HERE            
            if (validateStatus == true)
            {
                newLocation.Location1 = locationTextBox.Text;
                newLocation.State = stateTextBox.Text;
                context.Locations.Add(newLocation);
                context.SaveChanges();
                RefreshLocationList();
            }
        }

        private void RefreshLocationList()
        {
            locationsDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var LocationList = (from o in context.Locations
                              select o).ToList();
            locationsDataGrid.ItemsSource = LocationList;
        }

        private void BtnDeleteLocation_CLick(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Location?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Location nu = new Location { LocationID = myid };
                context.Locations.Attach(nu); //attaches the office object by the id given to the object above
                context.Locations.Remove(nu); //Adds the change to Deletes the Location from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshLocationList();
        }
        //****************************//
        //      END ADMIN LOCATIONS   //
        //****************************//

        //****************************//
        // Administration - Category  //
        //****************************//
        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Category newCategory = new Category();
            Boolean validateStatus = true;
            //DO VALIDATION CHECK HERE            
            if (validateStatus == true)
            {
                newCategory.CategoryName = categoryNameTextBox.Text;
                context.Categories.Add(newCategory);
                context.SaveChanges();
                RefreshCategoryList();
            }
        }

        private void RefreshCategoryList()
        {
            locationsDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var CategoryList = (from o in context.Categories
                                select o).ToList();
            categoriesDataGrid.ItemsSource = CategoryList;
        }

        private void BtnDeleteCategory_CLick(object sender, RoutedEventArgs e)
        {
            var context = new InventoryManagementSystem.InventoryDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Category?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Category nu = new Category { CategoryID = myid };
                context.Categories.Attach(nu); //attaches the office object by the id given to the object above
                context.Categories.Remove(nu); //Adds the change to Deletes the Category from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshCategoryList();
        }
        //****************************//
        //     END ADMIN CATEGORY     //
        //****************************//

        //****************************//
        //   Settings - Importer      //
        //****************************//
        private void BtnImportInventory_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtFileLink.Text)) //checks to make sure a file has been selected
            {
                try
                {
                    string filePath = TxtFileLink.Text;
                    var readcsv = System.IO.File.ReadAllText(filePath);

                    string[] csvfilerecord = readcsv.Split('\n');
                    var context = new InventoryManagementSystem.InventoryDBEntities();
                    int countTotal = csvfilerecord.Length;
                    int count = 0;
                    //MessageBox.Show(Convert.ToString(csvfilerecord.Length));
                    foreach (var row in csvfilerecord)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            count = count + 1;
                            var cells = row.Split(',');
                            //MessageBox.Show(cells[0] + ", \n" + cells[1] + cells[2] + ", \n" + cells[3] + ", \n" + cells[4] + ", \n" + cells[5] + ", \n" + cells[6] + ", \n" + cells[7] + ", \n" + cells[8] + ", \n" + cells[9] + ", \n" + cells[10] + ", \n" + cells[11] + ", \n" + cells[12] + ", \n" + cells[13] + ", \n" + cells[14] + ", \n" + cells[15] + ", \n" + cells[16] + ", \n" + cells[17] + ", \n" + cells[18] + ", " + cells[19]);

                            Inventory item = new Inventory //creates a new asset and put's the information from the csv into each field
                            {
                                itemName = cells[0],
                                tag = cells[1],
                                serialNumber = cells[2],
                            };

                            lblImportStatus.Content = row;
                            //lblDataUpdate.Refresh();
                            PbImportProgressBar.Value = (int)(100.0 * count / countTotal);

                            
                                context.Inventories.Add(item);
                                context.SaveChanges();

                        }
                        
                    }
                    PbImportProgressBar.Value = 100;
                    lblImportStatus.Content = "Done";
                    RefreshInventory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //MessageBox.Show("Please select a file first");
            }
        }

        //Brings up filedialog box and ask for a file.
        private void GetFile(TextBox textbox)
        {
            string path;
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel Files (*.csv)|*.csv";
            file.Multiselect = false;
            if (file.ShowDialog() == true)
            {
                path = file.FileName;
                textbox.Text = path;
            }
        }

        private void BtnPickFileImport_Click(object sender, RoutedEventArgs e)
        {
            GetFile(TxtFileLink);
        }

        private void tabAdmin_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            RefreshCategoryList();
            RefreshOfficeList();
            RefreshStatusList();
            RefreshLocationList();

        }
        //****************************//
        //    END SETTINGS IMPORTER   //
        //****************************//



    }
}
