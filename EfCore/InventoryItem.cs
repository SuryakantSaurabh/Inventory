using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingCrud.EfCore
{
    [Table("inventory_items")]
    public class InventoryItem
    {
        [Key] // Primary Key
        public int InventoryID { get; set; }

        [ForeignKey("Product")] // Assuming there's a Product entity with a ProductID as the foreign key
        public int ProductID { get; set; }

        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
