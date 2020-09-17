using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public class SqlShoppingListRepo : IShoppingListRepo
    {
        private readonly ShoppingListContext _context;

        public SqlShoppingListRepo(ShoppingListContext context)
        {
            _context = context;
        }

        public void CreateItem(Item itm)
        {
            if(itm == null)
            {
                throw new ArgumentNullException(nameof(itm));
            }
            _context.Items.Add(itm);
            
        }

        public void DeleteItem(Item itm)
        {
            if(itm == null)
            {
                throw new ArgumentNullException(nameof(itm));
            }
            _context.Items.Remove(itm);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }

        public Item GetItemByName(string name)
        {
            return _context.Items.FirstOrDefault(p => p.ItemName == name);
        }

        public IEnumerable<Item> GetItemsByStatus(string status)
        { 
            if(status == "true")
            {
                return _context.Items.Where(p => p.Status == true);
            }
            else
            {
                return _context.Items.Where(p => p.Status == false);
            }
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);
        }

        public void UpdateItem(Item itm)
        {
            
        }
    }
}