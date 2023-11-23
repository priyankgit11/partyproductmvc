using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyProductsMVC.Models
{
    [Table("tblProduct")]
    public class Product
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("product_name")]
        [MinLength(3, ErrorMessage = "Party Name must be 3 characters or more"), MaxLength(30, ErrorMessage = "BloggerName must be 30 characters or less"), ConcurrencyCheck]
        public string ProductName { get; set; }
        
        [Timestamp, Column("row_version")]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<AssignParty> AssignParties { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}