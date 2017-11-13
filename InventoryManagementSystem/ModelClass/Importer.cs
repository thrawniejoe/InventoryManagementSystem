using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.ModelClass
{
    class Importer
    {
        //Imports a CSV file into the database
        //Still needs some work
        //private void csvReader()
        //{
        //    if (!string.IsNullOrWhiteSpace(txtFilePath.Text)) //checks to make sure a file has been selected
        //    {
        //        try
        //        {
        //            string filePath = txtFilePath.Text;
        //            var readcsv = System.IO.File.ReadAllText(filePath);

        //            string[] csvfilerecord = readcsv.Split('\n');

        //            int countTotal = csvfilerecord.Length;
        //            int count = 0;
        //            //MessageBox.Show(Convert.ToString(csvfilerecord.Length));
        //            foreach (var row in csvfilerecord)
        //            {
        //                if (!string.IsNullOrEmpty(row))
        //                {
        //                    count = count + 1;
        //                    var cells = row.Split(',');
        //                    //MessageBox.Show(cells[0] + ", \n" + cells[1] + cells[2] + ", \n" + cells[3] + ", \n" + cells[4] + ", \n" + cells[5] + ", \n" + cells[6] + ", \n" + cells[7] + ", \n" + cells[8] + ", \n" + cells[9] + ", \n" + cells[10] + ", \n" + cells[11] + ", \n" + cells[12] + ", \n" + cells[13] + ", \n" + cells[14] + ", \n" + cells[15] + ", \n" + cells[16] + ", \n" + cells[17] + ", \n" + cells[18] + ", " + cells[19]);

        //                    var Inventory = new Inventory //creates a new asset and put's the information from the csv into each field
        //                    {
        //                        itemName = cells[0],
        //                        tag = cells[1],
        //                        serialNumber = cells[2],
        //                    };

        //                    lblDataUpdate.Text = row;
        //                    lblDataUpdate.Refresh();
        //                    progressBar1.Value = (int)(100.0 * count / countTotal);

        //                    var context = new InventoryManagementSystem.InventoryDBEntities();
        //                    using (var dbAsset = new InventoryDBDataSet())
        //                    {
        //                        context.Inventories.Add(Inventory);
        //                        context.SaveChanges();
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            //MessageBox.Show(e.Message);
        //        }
        //    }
        //    else
        //    {
        //        //MessageBox.Show("Please select a file first");
        //    }
        //}



    }
}
