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
    
    public partial class Documentation
    {
        public int DocID { get; set; }
        public int ItemID { get; set; }
        public string DocLink { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
    
        public virtual Inventory Inventory { get; set; }
    }
}
