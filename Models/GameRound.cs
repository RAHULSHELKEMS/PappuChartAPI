using System.ComponentModel.DataAnnotations;

namespace PappuPictureChart.API.Models
{
    public class GameRound
    {
        [Key]
        public int Id { get; set; }

        public int RoundNumber { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int? ResultPictureId { get; set; }

        public bool IsCompleted { get; set; }
    }
}
