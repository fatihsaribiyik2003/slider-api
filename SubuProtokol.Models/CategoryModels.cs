namespace SubuProtokol.Models
{
    public class CategoryCreate
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public string Description { get; set; }
    }

    public class CategoryQuery
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public string Description { get; set; }
    }

    public class CategoryUpdate
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
