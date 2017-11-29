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
    /// Interaction logic for OfficeMap.xaml
    /// </summary>
    public partial class OfficeMap : Window
    {
        public OfficeMap()
        {
            InitializeComponent();
        }

        private void LoadMap()
        {
            //Get Data From Offices Table then assign a username and office ID to all button text based on button tag
            var context = new InventoryManagementSystem.InventoryDBEntities();


            foreach (Button b in FindVisualChildren<Button>(this))
            {
                if (Convert.ToString(b.Tag) != "menuItem")
                {
                    string tag = b.Tag.ToString();
                    var selectedOffice = (from o in context.OfficeLists
                                          where o.officeName == tag
                                          select o.officeName).FirstOrDefault();

                    var empName = (from emp in context.Employees
                                   where emp.EmailAddress == selectedOffice
                                   select emp.Name).FirstOrDefault();

                    b.Content = b.Tag + "\r\n" + "\r\n" + empName;
                }
            }
        }


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        ////goes to asset lookup with selected office
        private void officeClick(object sender)
        {
            Button clickedButton = sender as Button;

            //gets the id for the record corrently selected
            string OfficeID = clickedButton.Tag.ToString();

            Views.ItemLookUp frmAssLUp = new Views.ItemLookUp();
            frmAssLUp.RequestType = "OfficeLookUp";
            frmAssLUp.officeNumber = OfficeID;
            //frmAssLUp.SendObjectId = dataid;
            frmAssLUp.Show();
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMap();
            lblCompanyName.Content = Properties.Settings.Default.CompanyName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            officeClick(sender);
        }
    }

    
}
