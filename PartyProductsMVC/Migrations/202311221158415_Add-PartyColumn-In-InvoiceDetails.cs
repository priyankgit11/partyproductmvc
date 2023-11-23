namespace PartyProductsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPartyColumnInInvoiceDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblInvoiceDetail", "party_id", c => c.Int(nullable: false));
            CreateIndex("dbo.tblInvoiceDetail", "party_id");
            AddForeignKey("dbo.tblInvoiceDetail", "party_id", "dbo.tblParty", "id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblInvoiceDetail", "party_id", "dbo.tblParty");
            DropIndex("dbo.tblInvoiceDetail", new[] { "party_id" });
            DropColumn("dbo.tblInvoiceDetail", "party_id");
        }
    }
}
