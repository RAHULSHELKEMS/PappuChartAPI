namespace PappuPictureChart.API.Models;

public class Result
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public string ResultNumber { get; set; } = string.Empty;

    public DateTime ResultDate { get; set; } = DateTime.Now;
}