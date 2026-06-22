namespace PappuPictureChart.API.Models
{
    public class Chart
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public string Result { get; set; } = string.Empty;

        public DateTime ResultDate { get; set; }

        public Game? Game { get; set; }
    }
}
