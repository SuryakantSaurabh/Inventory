using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingCrud.EfCore
{
    [Table("suppliers")] // Table name for the Supplier entity
    public class Supplier
    {
        [Key] // Primary Key
        public int SupplierID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SupplierName { get; set; }

        [MaxLength(100)]
        public string ContactName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string ContactEmail { get; set; }

        [MaxLength(20)]
        public string ContactPhone { get; set; }
    }
}
