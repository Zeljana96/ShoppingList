using System.Collections.Generic;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public class MockShoppingListRepo : IShoppingListRepo
    {
        public void CreateItem(Item itm)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteItem(Item itm)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Item> GetAllItems()
        {
            var items = new List<Item>
            {
                new Item{Id=0, ItemName="shampoo", Required=2, Provided=1, Status=false},
                new Item{Id=1, ItemName="nutella", Required=3, Provided=0, Status=false},
                new Item{Id=2, ItemName="ball", Required=1, Provided=1, Status=true},
                new Item{Id=3, ItemName="tequila", Required=5, Provided=3, Status=false}
            };

            return items;
        }

        public Item GetItemByName(string name)
        {
            return new Item{Id=0, ItemName="shampoo", Required=2, Provided=1, Status=false};
        }

        public IEnumerable<Item> GetItemsByStatus(string status)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateItem(Item itm)
        {
            throw new System.NotImplementedException();
        }
    }
}