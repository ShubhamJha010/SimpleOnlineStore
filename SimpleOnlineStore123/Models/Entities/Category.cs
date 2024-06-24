using SimpleOnlineStore123.Models.Entities;

namespace SimpleOnlineStore123.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}