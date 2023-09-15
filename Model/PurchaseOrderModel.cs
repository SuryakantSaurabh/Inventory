using System;

namespace TestingCrud.Model
{
    public class PurchaseOrderModel
    {
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }
        public string Status { get; set; }
    }
}
