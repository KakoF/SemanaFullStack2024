
namespace Financial.Core.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string UserId { get; set; } = string.Empty;

        public Category(string title, string? description, string userId)
        {
            Title = title;
            Description = description;
            UserId = userId;
        }

        public void Update(string title, string description)
        {
           Title = title;
            Description = description;
        }
    }
}
