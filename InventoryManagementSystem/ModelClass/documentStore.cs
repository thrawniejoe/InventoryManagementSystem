using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagementSystem.ModelClass
{
    class DocumentStore
    {
        List<string> statusList = new List<string>();

        //Adds a document for the current record
        public void AddDocToDB(string path, string filename, int tag)
        {
            try
            {
                var context = new InventoryManagementSystem.InventoryDBEntities();

                Documentation newDoc = new Documentation();
                newDoc.DocLink = path;
                newDoc.ItemID = tag;
                newDoc.DateAdded = DateTime.Now;
                newDoc.DocLink = filename;
                context.Documentations.Add(newDoc);
                context.SaveChanges();


                // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-copy-delete-and-move-files-and-folders

                MessageBox.Show(newDoc.ItemID + " has been Added to the database for item " + tag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //status list
        public List<string> GetStatusList()
        {
            return statusList;
        }

        public void AddStatus(string status)
        {
            statusList.Add(status);
        }
    }
}
