using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Dtos
{
    public class ItemCreateDto
    {
        [Required]
        public string ItemName { get; set; }
        
        [Required]
        public int Required { get; set; }

        [Required]
        public int Provided { get; set; }
        public bool Status { get; set; }
    }
}


