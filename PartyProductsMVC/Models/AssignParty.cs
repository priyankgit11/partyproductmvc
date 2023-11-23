using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyProductsMVC.Models
{
    [Table("tblAssignParty")]
    public class AssignParty
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("party_id")]
        [ForeignKey("Party")]
        public int PartyID { get; set; }
        public virtual Party Party { get; set; }

        [Required, Column("product_id")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        [Timestamp, Column("row_version")]
        public byte[] RowVersion { get; set; }
    }
}