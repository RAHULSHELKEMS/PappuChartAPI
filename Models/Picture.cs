using System.ComponentModel.DataAnnotations;

namespace PappuPictureChart.API.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Rate { get; set; } = 10;
    }
}
