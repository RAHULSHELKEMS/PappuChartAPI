using System.ComponentModel.DataAnnotations;

namespace PappuPictureChart.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public decimal Coins { get; set; } = 100;

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
