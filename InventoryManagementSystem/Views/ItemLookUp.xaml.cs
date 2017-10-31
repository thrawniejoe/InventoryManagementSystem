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
    /// Interaction logic for ItemLookUp.xaml
    /// </summary>
    public partial class ItemLookUp : Window
    {
        public ItemLookUp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            InventoryManagementSystem.InventoryDBDataSet inventoryDBDataSet = ((InventoryManagementSystem.InventoryDBDataSet)(this.FindResource("inventoryDBDataSet")));
            // Load data into the table vInventoryList. You can modify this code as needed.
            InventoryManagementSystem.InventoryDBDataSetTableAdapters.vInventoryListTableAdapter inventoryDBDataSetvInventoryListTableAdapter = new InventoryManagementSystem.InventoryDBDataSetTableAdapters.vInventoryListTableAdapter();
            inventoryDBDataSetvInventoryListTableAdapter.Fill(inventoryDBDataSet.vInventoryList);
            System.Windows.Data.CollectionViewSource vInventoryListViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("vInventoryListViewSource")));
            vInventoryListViewSource.View.MoveCurrentToFirst();
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
        }


        //Loads Data
        private void LoadDat()
        {
            cboEmplyeeList.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var EmployeeList_Name = (from r in context.Employees
                            select r.Username).ToList();

            cboEmplyeeList.ItemsSource = EmployeeList_Name;


            var Office = (from r in context.OfficeLists
                                     select r.officeName).ToList();

            cboOfficeList.ItemsSource = Office;

            var TagNumber = (from r in context.Inventories
                          select r.tag).ToList();

            cboTagList.ItemsSource = TagNumber;

        }

        private void InventoryListView()
        {
            vInventoryListDataGrid.ItemsSource = null;
            var context = new InventoryManagementSystem.InventoryDBEntities();
            var InventoryList = (from i in context.vInventoryLists
                                     select i).ToList();
            vInventoryListDataGrid.ItemsSource = InventoryList;
        }
    }
}
