using System;
using System.Collections.Generic;
using System.Linq;
using TestingCrud.EfCore;

namespace TestingCrud.Model
{
    public class DbHelperPurchaseOrder
    {
        private EF_DataContext _context;

        public DbHelperPurchaseOrder(EF_DataContext context)
        {
            _context = context;
        }

        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return _context.PurchaseOrders.ToList();
        }

        public PurchaseOrder GetPurchaseOrderById(int id)
        {
            return _context.PurchaseOrders.Find(id);
        }

        public void CreatePurchaseOrder(PurchaseOrder model)
        {
            _context.PurchaseOrders.Add(model);
            _context.SaveChanges();
        }

        public void UpdatePurchaseOrder(PurchaseOrder model)
        {
            _context.PurchaseOrders.Update(model);
            _context.SaveChanges();
        }

        public void DeletePurchaseOrder(int id)
        {
            var purchaseOrder = _context.PurchaseOrders.Find(id);
            if (purchaseOrder != null)
            {
                _context.PurchaseOrders.Remove(purchaseOrder);
                _context.SaveChanges();
            }
        }
    }
}
