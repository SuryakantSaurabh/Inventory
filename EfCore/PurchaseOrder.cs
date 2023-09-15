using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingCrud.EfCore
{
    [Table("purchase_orders")]
    public class PurchaseOrder
    {
        [Key]
        public int OrderID { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime ExpectedArrivalDate { get; set; }

        [Required]
        public string Status { get; set; }

        // Navigation property to the Supplier entity
        public virtual Supplier Supplier { get; set; }
    }
}
