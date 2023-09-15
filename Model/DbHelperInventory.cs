using System;
using System.Collections.Generic;
using System.Linq;
using TestingCrud.EfCore;

namespace TestingCrud.Model
{
    public class DbHelperInventory
    {
        private EF_DataContext _context;

        public DbHelperInventory(EF_DataContext context)
        {
            _context = context;
        }

        public List<InventoryItemModel> GetInventoryItems()
        {
            return _context.InventoryItems
                .Select(item => new InventoryItemModel
                {
                    InventoryID = item.InventoryID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Location = item.Location,
                    ExpiryDate = item.ExpiryDate
                })
                .ToList();
        }

        public InventoryItemModel GetInventoryItemById(int id)
        {
            var item = _context.InventoryItems.Find(id);
            if (item != null)
            {
                return new InventoryItemModel
                {
                    InventoryID = item.InventoryID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Location = item.Location,
                    ExpiryDate = item.ExpiryDate
                };
            }
            return null;
        }

        public void SaveInventoryItem(InventoryItemModel itemModel)
        {
            var item = new InventoryItem
            {
                ProductID = itemModel.ProductID,
                Quantity = itemModel.Quantity,
                Location = itemModel.Location,
                ExpiryDate = itemModel.ExpiryDate.HasValue
            ? itemModel.ExpiryDate.Value.ToUniversalTime() // Convert to UTC if not null
            : (DateTime?)null // Set as null if it's null in the model
            };
            _context.InventoryItems.Add(item);
            _context.SaveChanges();
        }

        public void UpdateInventoryItem(InventoryItemModel itemModel)
        {
            var item = _context.InventoryItems.Find(itemModel.InventoryID);
            if (item != null)
            {
                item.ProductID = itemModel.ProductID;
                item.Quantity = itemModel.Quantity;
                item.Location = itemModel.Location;
                item.ExpiryDate = itemModel.ExpiryDate;
                _context.SaveChanges();
            }
        }

        public void DeleteInventoryItem(int id)
        {
            var item = _context.InventoryItems.Find(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
