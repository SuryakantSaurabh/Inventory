using System;
using System.Collections.Generic;
using System.Linq;
using TestingCrud.EfCore;

namespace TestingCrud.Model
{
    public class DbHelperSupplier
    {
        private EF_DataContext _context;

        public DbHelperSupplier(EF_DataContext context)
        {
            _context = context;
        }

        public List<SupplierModel> GetSuppliers()
        {
            return _context.Suppliers
                .Select(supplier => new SupplierModel
                {
                    SupplierID = supplier.SupplierID,
                    SupplierName = supplier.SupplierName,
                    ContactName = supplier.ContactName,
                    ContactEmail = supplier.ContactEmail,
                    ContactPhone = supplier.ContactPhone
                })
                .ToList();
        }

        public SupplierModel GetSupplierById(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                return new SupplierModel
                {
                    SupplierID = supplier.SupplierID,
                    SupplierName = supplier.SupplierName,
                    ContactName = supplier.ContactName,
                    ContactEmail = supplier.ContactEmail,
                    ContactPhone = supplier.ContactPhone
                };
            }
            return null;
        }

        public void SaveSupplier(SupplierModel supplierModel)
        {
            var supplier = new Supplier
            {
                SupplierName = supplierModel.SupplierName,
                ContactName = supplierModel.ContactName,
                ContactEmail = supplierModel.ContactEmail,
                ContactPhone = supplierModel.ContactPhone
            };
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void UpdateSupplier(SupplierModel supplierModel)
        {
            var supplier = _context.Suppliers.Find(supplierModel.SupplierID);
            if (supplier != null)
            {
                supplier.SupplierName = supplierModel.SupplierName;
                supplier.ContactName = supplierModel.ContactName;
                supplier.ContactEmail = supplierModel.ContactEmail;
                supplier.ContactPhone = supplierModel.ContactPhone;
                _context.SaveChanges();
            }
        }

        public void DeleteSupplier(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                _context.SaveChanges();
            }
        }
    }
}
