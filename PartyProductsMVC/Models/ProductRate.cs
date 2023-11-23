using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyProductsMVC.Models
{
    [Table("tblProductRate")]
    public class ProductRate
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("product_id")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [Column("rate",TypeName="money"),ConcurrencyCheck]
        public decimal Rate { get; set; }
    }
}