using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyProductsMVC.Models
{
    [Table("tblInvoice")]
    public class Invoice
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Party"), Column("party_id")]
        public int PartyID { get; set; }
        public virtual Party Party { get; set; }

        [Required]
        [Column("grand_total", TypeName ="money")]
        public decimal GrandTotal { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}