namespace Shop_MVC_VPD_121.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ---------- navigation properties
        public ICollection<Product> Products { get; set; }
    }
}
