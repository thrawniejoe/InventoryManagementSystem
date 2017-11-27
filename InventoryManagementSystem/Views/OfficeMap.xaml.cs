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
            //    var context = new InventoryManagementSystem.InventoryDBEntities();

            //    foreach (Control b in this.Controls)
            //    {
            //        if (b.Tag != "menuItem")
            //        {
            //            var selectedOffice = (from o in context.Offices
            //                                  where o.Office1 == b.Tag
            //                                  select o.AssignedUser).FirstOrDefault();

            //            var empName = (from emp in context.Employees
            //                           where emp.emailAddress == selectedOffice
            //                           select emp.employeeName).FirstOrDefault();

            //            b.Text = b.Tag + "\r\n" + "\r\n" + empName;
            //        }
            //    }
            //}

            //private void button1_Click(object sender, EventArgs e)
            //{
            //    officeClick(sender);
            //}



            ////goes to asset lookup with selected office
            //private void officeClick(object sender)
            //{
            //    Button clickedButton = sender as Button;

            //    //gets the id for the record corrently selected
            //    string OfficeID = clickedButton.Tag.ToString();

            //    AssetLookUp frmAssLUp = new AssetLookUp();
            //    frmAssLUp.JobReq = "OfficeID";
            //    frmAssLUp.OfficeId = OfficeID;
            //    //frmAssLUp.SendObjectId = dataid;
            //    frmAssLUp.MdiParent = MainMenu.ActiveForm;
            //    frmAssLUp.Show();
            //    this.Close();
            //}

            //private void btnClose_Click(object sender, EventArgs e)
            //{
            //    this.Close();
            //}
        }

    }
}
