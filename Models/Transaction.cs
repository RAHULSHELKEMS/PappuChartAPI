using System.ComponentModel.DataAnnotations;

namespace PappuPictureChart.API.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Coins { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
