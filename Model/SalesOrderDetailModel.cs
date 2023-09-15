using System.ComponentModel.DataAnnotations;

namespace TestingCrud.Model
{
    public class SalesOrderDetailModel
    {
        public int OrderDetailID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
