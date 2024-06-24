using SimpleOnlineStore123.Models.Entities;

namespace SimpleOnlineStore123.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}

