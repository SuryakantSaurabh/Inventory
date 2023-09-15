using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingCrud.EfCore
{
    [Table("products")] // Renamed table to "products" to match the pluralized entity name.
    public class Product
    {
        [Key] // Primary Key
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required]
        public string UnitOfMeasure { get; set; }

        public decimal Price { get; set; }

        // Define the navigation property for orders if you have a relationship with the Order entity.
       
    }
}
