using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestingCrud.EfCore
{
        [Table("sales_orders")] // Adjust table name as needed
        public class SalesOrder
        {
            [Key] // Primary Key
            public int OrderID { get; set; }

            [Required]
            public string CustomerName { get; set; }

            [Required]
            public DateTime OrderDate { get; set; }

            [Required]
            public DateTime DeliveryDate { get; set; }

            [Required]
            public string Status { get; set; }
        }
    }

