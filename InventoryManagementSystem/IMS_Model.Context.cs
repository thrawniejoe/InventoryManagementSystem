﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InventoryDBEntities : DbContext
    {
        public InventoryDBEntities()
            : base("name=InventoryDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ComputerSpecsList> ComputerSpecsLists { get; set; }
        public virtual DbSet<Documentation> Documentations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<OfficeList> OfficeLists { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<vInventoryList> vInventoryLists { get; set; }
    }
}
