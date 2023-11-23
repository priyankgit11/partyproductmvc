namespace PartyProductsMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignParties",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        party_id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        row_version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblParty", t => t.party_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.party_id)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.tblParty",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        party_name = c.String(nullable: false, maxLength: 30),
                        row_version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_name = c.String(nullable: false, maxLength: 30),
                        row_version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        rate = c.Decimal(nullable: false, storeType: "money"),
                        quantity = c.Int(nullable: false),
                        invoice_id = c.Int(nullable: false),
                        row_version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Invoices", t => t.invoice_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.invoice_id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        party_id = c.Int(nullable: false),
                        grand_total = c.Decimal(nullable: false, storeType: "money"),
                        row_version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.tblParty", t => t.party_id, cascadeDelete: true)
                .Index(t => t.party_id);
            
            CreateTable(
                "dbo.ProductRates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        rate = c.Decimal(nullable: false, storeType: "money"),
                        row_version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Products", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductRates", "product_id", "dbo.Products");
            DropForeignKey("dbo.InvoiceDetails", "product_id", "dbo.Products");
            DropForeignKey("dbo.InvoiceDetails", "invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "party_id", "dbo.tblParty");
            DropForeignKey("dbo.AssignParties", "product_id", "dbo.Products");
            DropForeignKey("dbo.AssignParties", "party_id", "dbo.tblParty");
            DropIndex("dbo.ProductRates", new[] { "product_id" });
            DropIndex("dbo.Invoices", new[] { "party_id" });
            DropIndex("dbo.InvoiceDetails", new[] { "invoice_id" });
            DropIndex("dbo.InvoiceDetails", new[] { "product_id" });
            DropIndex("dbo.AssignParties", new[] { "product_id" });
            DropIndex("dbo.AssignParties", new[] { "party_id" });
            DropTable("dbo.ProductRates");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceDetails");
            DropTable("dbo.Products");
            DropTable("dbo.tblParty");
            DropTable("dbo.AssignParties");
        }
    }
}
