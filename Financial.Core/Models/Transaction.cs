using Financial.Core.Enums;

namespace Financial.Core.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PaidOrReceivedAt { get; set; }
        public eTransactionType Type { get; set; } = eTransactionType.Withdraw;
        public decimal Amount { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;

        public Transaction(string title, DateTime? paidOrReceivedAt, eTransactionType type, decimal amount, long categoryId, string userId)
        {
            Title = title;
            PaidOrReceivedAt = paidOrReceivedAt;
            Type = type;
            Amount = amount;
            CategoryId = categoryId;
            UserId = userId;
        }

        public void Update(long categoryId, decimal amount, string title, eTransactionType type, DateTime? paidOrReceivedAt)
        {
            CategoryId = categoryId;
            Amount = amount;
            Title = title;
            Type = type;
            PaidOrReceivedAt = paidOrReceivedAt;
        }
    }
}
