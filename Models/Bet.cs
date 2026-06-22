using System.ComponentModel.DataAnnotations;

namespace PappuPictureChart.API.Models
{
    public class Bet
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoundId { get; set; }

        public int PictureId { get; set; }

        public decimal Coins { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
    public class BetRequest
    {
        public int RoundId { get; set; }

        public List<PictureBet> Bets { get; set; } = new();
    }
    public class PictureBet
    {
        public int PictureId { get; set; }

        public decimal Coins { get; set; }
    }
}
