using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartyProductsMVC.Models
{
    [Table("tblParty")]
    public class Party
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("party_name")]
        [MinLength(3, ErrorMessage = "Party Name must be 3 characters or more"), MaxLength(30, ErrorMessage = "BloggerName must be 30 characters or less"), ConcurrencyCheck]
        public string PartyName { get; set; }
        [Timestamp, Column("row_version")]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<AssignParty> AssignParties { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}