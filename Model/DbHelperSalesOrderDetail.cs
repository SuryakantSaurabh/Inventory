using System;
using System.Collections.Generic;
using System.Linq;
using TestingCrud.EfCore;

namespace TestingCrud.Model
{
    public class DbHelperSalesOrderDetail
    {
        private EF_DataContext _context;

        public DbHelperSalesOrderDetail(EF_DataContext context)
        {
            _context = context;
        }

        public List<SalesOrderDetailModel> GetSalesOrderDetails()
        {
            return _context.SalesOrderDetails
                .Select(detail => new SalesOrderDetailModel
                {
                    OrderDetailID = detail.OrderDetailID,
                    OrderID = detail.OrderID,
                    ProductID = detail.ProductID,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                })
                .ToList();
        }

        public SalesOrderDetailModel GetSalesOrderDetailById(int id)
        {
            var detail = _context.SalesOrderDetails.Find(id);
            if (detail != null)
            {
                return new SalesOrderDetailModel
                {
                    OrderDetailID = detail.OrderDetailID,
                    OrderID = detail.OrderID,
                    ProductID = detail.ProductID,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice
                };
            }
            return null;
        }

        public void CreateSalesOrderDetail(SalesOrderDetailModel detailModel)
        {
            var detail = new SalesOrderDetail
            {
                OrderID = detailModel.OrderID,
                ProductID = detailModel.ProductID,
                Quantity = detailModel.Quantity,
                UnitPrice = detailModel.UnitPrice
            };
            _context.SalesOrderDetails.Add(detail);
            _context.SaveChanges();
        }

        public void UpdateSalesOrderDetail(SalesOrderDetailModel detailModel)
        {
            var detail = _context.SalesOrderDetails.Find(detailModel.OrderDetailID);
            if (detail != null)
            {
                detail.OrderID = detailModel.OrderID;
                detail.ProductID = detailModel.ProductID;
                detail.Quantity = detailModel.Quantity;
                detail.UnitPrice = detailModel.UnitPrice;
                _context.SaveChanges();
            }
        }

        public void DeleteSalesOrderDetail(int id)
        {
            var detail = _context.SalesOrderDetails.Find(id);
            if (detail != null)
            {
                _context.SalesOrderDetails.Remove(detail);
                _context.SaveChanges();
            }
        }
    }
}
