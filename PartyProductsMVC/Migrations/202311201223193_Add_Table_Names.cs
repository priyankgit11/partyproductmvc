namespace PartyProductsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_Names : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AssignParties", newName: "tblAssignParty");
            RenameTable(name: "dbo.Products", newName: "tblProduct");
            RenameTable(name: "dbo.InvoiceDetails", newName: "tblInvoiceDetail");
            RenameTable(name: "dbo.Invoices", newName: "tblInvoice");
            RenameTable(name: "dbo.ProductRates", newName: "tblProductRate");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tblProductRate", newName: "ProductRates");
            RenameTable(name: "dbo.tblInvoice", newName: "Invoices");
            RenameTable(name: "dbo.tblInvoiceDetail", newName: "InvoiceDetails");
            RenameTable(name: "dbo.tblProduct", newName: "Products");
            RenameTable(name: "dbo.tblAssignParty", newName: "AssignParties");
        }
    }
}
