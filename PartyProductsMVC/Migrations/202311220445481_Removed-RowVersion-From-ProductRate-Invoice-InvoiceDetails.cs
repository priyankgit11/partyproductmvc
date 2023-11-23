namespace PartyProductsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRowVersionFromProductRateInvoiceInvoiceDetails : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblInvoiceDetail", "row_version");
            DropColumn("dbo.tblInvoice", "row_version");
            DropColumn("dbo.tblProductRate", "row_version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblProductRate", "row_version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.tblInvoice", "row_version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.tblInvoiceDetail", "row_version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
