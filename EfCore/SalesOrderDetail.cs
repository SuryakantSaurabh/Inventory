using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingCrud.EfCore
{
    [Table("sales_order_details")]
    public class SalesOrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Required]
        [ForeignKey("SalesOrder")]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public SalesOrder SalesOrder { get; set; }
        public Product Product { get; set; }
    }
}
