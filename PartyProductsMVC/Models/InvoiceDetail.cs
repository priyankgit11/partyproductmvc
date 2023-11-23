using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyProductsMVC.Models
{
    [Table("tblInvoiceDetail")]
    public class InvoiceDetail
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("party_id")]
        [ForeignKey("Party")]
        public int? PartyID { get; set; }
        public virtual Party Party { get; set; }

        [Required, Column("product_id")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Required, Column("rate", TypeName = "money")]
        public decimal Rate { get; set; }

        [Required,Column("quantity")]
        public int Quantity { get; set; }

        [Required, Column("total", TypeName = "money")]
        public decimal Total { get; set; }

        [Column("invoice_id"), ForeignKey("Invoices")]
        public int InvoiceID { get; set; }
        public virtual Invoice Invoices { get; set; }
    }
}