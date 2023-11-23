using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PartyProductsMVC.Models;

namespace PartyProductsMVC.DAL
{
    public class PartyProductContext : DbContext
    {
        public PartyProductContext() : base("PartyProductMVCDB")
        {
            Database.SetInitializer(new PartyProductDBInitializer());
        }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<AssignParty> AssignParties { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}