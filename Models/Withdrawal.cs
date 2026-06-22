using System.ComponentModel.DataAnnotations;

namespace PappuPictureChart.API.Models
{
    public class Withdrawal
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public string UpiId { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
