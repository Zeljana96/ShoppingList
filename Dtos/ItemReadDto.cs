namespace ShoppingList.Dtos
{
    public class ItemReadDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Required { get; set; }
        public int Provided { get; set; }
        public bool Status { get; set; }
    }
}


