using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyProductsMVC.Models;

namespace PartyProductsMVC.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int InvoiceID { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public bool IsDisabled { get; set; }
        //public IEnumerable<InvoiceViewModel> InvoiceViewModels { get; set; }
        public IEnumerable<InvoiceDetail> InvoiceDetails { get; set; }
    }
}