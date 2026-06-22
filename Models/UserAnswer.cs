namespace PappuPictureChart.API.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoundId { get; set; }
        public int SelectedPictureId { get; set; }
    }
}
