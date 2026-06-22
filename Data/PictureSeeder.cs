using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Data
{
    public static class PictureSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Pictures.Any())
                return;

            var pictures = new List<Picture>
            {
                new() { Id=1, Name="Chhata", ImageUrl="/Picture/chhata1.png", Rate=10 },
                new() { Id=2, Name="Ball", ImageUrl="/Picture/ball2.png", Rate=10 },
                new() { Id=3, Name="Suraj", ImageUrl="/Picture/suraj3.png", Rate=10 },
                new() { Id=4, Name="Diya", ImageUrl="/Picture/diya4.png", Rate=10 },
                new() { Id=5, Name="Gai", ImageUrl="/Picture/gai5.png", Rate=10 },
                new() { Id=6, Name="Balti", ImageUrl="/Picture/balti6.png", Rate=10 },
                new() { Id=7, Name="Patang", ImageUrl="/Picture/patang7.png", Rate=10 },
                new() { Id=8, Name="Lattu", ImageUrl="/Picture/lattu8.png", Rate=10 },
                new() { Id=9, Name="Ghulab", ImageUrl="/Picture/ghulab9.png", Rate=10 },
                new() { Id=10, Name="Titali", ImageUrl="/Picture/titali10.png", Rate=10 },
                new() { Id=11, Name="Kabutar", ImageUrl="/Picture/kabutar11.png", Rate=10 },
                new() { Id=12, Name="Kharghosh", ImageUrl="/Picture/kharghosh12.png", Rate=10 }
            };

            db.Pictures.AddRange(pictures);

            await db.SaveChangesAsync();
        }
    }
}