using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PartyProductsMVC.Models;

namespace PartyProductsMVC.DAL
{
    public class PartyProductDBInitializer : DropCreateDatabaseAlways<PartyProductContext>
    {
        protected override void Seed(PartyProductContext context)
        {
            IList<Party> defaultParty = new List<Party>();
            defaultParty.Add(new Party { PartyName = "Priyank" });
            defaultParty.Add(new Party { PartyName = "Dave" });
            context.Parties.AddRange(defaultParty);

            IList<Product> defaultProduct = new List<Product>();
            defaultProduct.Add(new Product { ProductName = "apple" });
            defaultProduct.Add(new Product { ProductName = "jaljira" });
            context.Products.AddRange(defaultProduct);
            context.SaveChanges();

            IList<AssignParty> defaultAssignParty = new List<AssignParty>();
            defaultAssignParty.Add(new AssignParty { PartyID = 1, ProductID = 1 });
            defaultAssignParty.Add(new AssignParty { PartyID = 1, ProductID = 2 });
            context.AssignParties.AddRange(defaultAssignParty);

            IList<ProductRate> defaultRate = new List<ProductRate>();
            defaultRate.Add(new ProductRate { ProductID=1, Rate = 35 });
            defaultRate.Add(new ProductRate { ProductID=2, Rate = 23 });
            context.ProductRates.AddRange(defaultRate);

            base.Seed(context);
        }
    }
}