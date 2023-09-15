namespace TestingCrud.Model
{
    public class InventoryItemModel
    {
        public int InventoryID { get; set; } // Primary Key
        public int ProductID { get; set; } // Foreign Key
        public int Quantity { get; set; }
        public string Location { get; set; }
        public DateTime? ExpiryDate { get; set; } // Nullable DateTime
    }
}
