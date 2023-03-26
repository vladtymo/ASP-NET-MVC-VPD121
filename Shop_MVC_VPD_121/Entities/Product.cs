namespace Shop_MVC_VPD_121.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        // ---------- navigation properties
        public Category Category { get; set; }
    }
}
