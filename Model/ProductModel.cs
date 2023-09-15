namespace TestingCrud.Model
{
    public class ProductModel
    {
        //internal int id;

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string UnitOfMeasure { get; set; }

        public decimal Price { get; set; }
    }
}
