namespace PartyProductsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalColumnIntblInvoiceDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblInvoiceDetail", "total", c => c.Decimal(nullable: false, storeType: "money"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblInvoiceDetail", "total");
        }
    }
}
