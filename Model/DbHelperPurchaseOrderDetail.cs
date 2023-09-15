using System;
using System.Collections.Generic;
using System.Linq;
using TestingCrud.EfCore;

namespace TestingCrud.Model
{
    public class DbHelperPurchaseOrderDetail
    {
        private EF_DataContext _context;

        public DbHelperPurchaseOrderDetail(EF_DataContext context)
        {
            _context = context;
        }

        public List<PurchaseOrderDetail> GetPurchaseOrderDetails()
        {
            return _context.PurchaseOrderDetails.ToList();
        }

        public PurchaseOrderDetail GetPurchaseOrderDetailById(int id)
        {
            return _context.PurchaseOrderDetails.FirstOrDefault(detail => detail.OrderDetailID == id);
        }

        public void CreatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            _context.PurchaseOrderDetails.Add(purchaseOrderDetail);
            _context.SaveChanges();
        }

        public void UpdatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            _context.PurchaseOrderDetails.Update(purchaseOrderDetail);
            _context.SaveChanges();
        }

        public void DeletePurchaseOrderDetail(int id)
        {
            var purchaseOrderDetail = _context.PurchaseOrderDetails.FirstOrDefault(detail => detail.OrderDetailID == id);
            if (purchaseOrderDetail != null)
            {
                _context.PurchaseOrderDetails.Remove(purchaseOrderDetail);
                _context.SaveChanges();
            }
        }
    }
}
