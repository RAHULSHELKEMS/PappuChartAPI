namespace PappuPictureChart.API.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string GameName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
