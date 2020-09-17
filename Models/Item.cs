using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public int Required { get; set; }

        [Required]
        public int Provided { get; set; }

        public bool Status { get; set; }
    }
}


