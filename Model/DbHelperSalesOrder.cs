using System;
using System.Collections.Generic;
using System.Linq;
using TestingCrud.EfCore;

namespace TestingCrud.Model
{
    public class DbHelperSalesOrder
    {
        private EF_DataContext _context;

        public DbHelperSalesOrder(EF_DataContext context)
        {
            _context = context;
        }

        public List<SalesOrderModel> GetSalesOrders()
        {
            return _context.SalesOrders
                .Select(order => new SalesOrderModel
                {
                    OrderID = order.OrderID,
                    CustomerName = order.CustomerName,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Status = order.Status
                })
                .ToList();
        }

        public SalesOrderModel GetSalesOrderById(int id)
        {
            var order = _context.SalesOrders.Find(id);
            if (order != null)
            {
                return new SalesOrderModel
                {
                    OrderID = order.OrderID,
                    CustomerName = order.CustomerName,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate,
                    Status = order.Status
                };
            }
            return null;
        }

        public void CreateSalesOrder(SalesOrderModel orderModel)
        {
            var order = new SalesOrder
            {
                CustomerName = orderModel.CustomerName,
                OrderDate = orderModel.OrderDate,
                DeliveryDate = orderModel.DeliveryDate,
                Status = orderModel.Status
            };
            _context.SalesOrders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateSalesOrder(SalesOrderModel orderModel)
        {
            var order = _context.SalesOrders.Find(orderModel.OrderID);
            if (order != null)
            {
                order.CustomerName = orderModel.CustomerName;
                order.OrderDate = orderModel.OrderDate;
                order.DeliveryDate = orderModel.DeliveryDate;
                order.Status = orderModel.Status;
                _context.SaveChanges();
            }
        }

        public void DeleteSalesOrder(int id)
        {
            var order = _context.SalesOrders.Find(id);
            if (order != null)
            {
                _context.SalesOrders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
