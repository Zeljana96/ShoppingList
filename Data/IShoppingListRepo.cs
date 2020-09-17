using System.Collections.Generic;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public interface IShoppingListRepo
    {
        bool SaveChanges();
        IEnumerable<Item> GetAllItems();
        Item GetItemByName(string name);
        void CreateItem(Item itm);

        void UpdateItem(Item itm);

        void DeleteItem(Item itm);

        IEnumerable<Item> GetItemsByStatus(string status);
    }
}