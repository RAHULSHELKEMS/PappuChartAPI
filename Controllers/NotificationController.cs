using Microsoft.AspNetCore.Mvc;

namespace PappuPictureChart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Notifications List");
    }

    [HttpPost]
    public IActionResult Send()
    {
        return Ok("Notification Sent");
    }
}