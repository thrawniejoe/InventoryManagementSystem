//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inventory
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        public string tag { get; set; }
        public string serialNumber { get; set; }
        public string manufacturer { get; set; }
        public Nullable<int> modelID { get; set; }
        public string modelNumber { get; set; }
        public string category { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public Nullable<int> assignedTo { get; set; }
        public Nullable<System.DateTime> dateAssigned { get; set; }
        public Nullable<int> documentationID { get; set; }
        public Nullable<System.DateTime> datePurchased { get; set; }
        public string assignedLocation { get; set; }
    
        public virtual ComputerSpecsList ComputerSpecsList { get; set; }
        public virtual Documentation Documentation { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
