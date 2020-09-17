using Microsoft.EntityFrameworkCore;
using ShoppingList.Models;

namespace ShoppingList.Data
{
    public class ShoppingListContext : DbContext
    {
        public ShoppingListContext(DbContextOptions<ShoppingListContext> opt) : base(opt)
        {
            
        }

        public DbSet<Item> Items { get; set; }
    }
}