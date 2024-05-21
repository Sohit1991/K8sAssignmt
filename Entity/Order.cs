namespace ProductOrdering.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? UserName { get; set; }
        public string? EmailAddress { get; set; } 

    }
}
