using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    [HttpPost("create-order")]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            RazorpayClient client =
                new RazorpayClient(
                      "rzp_test_T2i5foOFnN8Zaj",
                      "yrTO7aywXy21hvR2h6ic8YCW");

            Dictionary<string, object> options = new();

            options.Add(
                "amount",
                Convert.ToInt32(request.Amount * 100));

            options.Add(
                "currency",
                "INR");

            options.Add(
                "receipt",
                Guid.NewGuid().ToString("N").Substring(0, 20));

            Order order =
                client.Order.Create(options);

            return Ok(new
            {
                orderId = order["id"].ToString(),
                amount = order["amount"].ToString(),
                key = "rzp_test_T2i5foOFnN8Zaj"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateOrderRequest
{
    public decimal Amount { get; set; }
}


//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json.Linq;
//using PappuPictureChart.API.Data;
//using Razorpay.Api;

//[ApiController]
//[Route("api/payment")]
//public class PaymentController : ControllerBase
//{

//    private readonly ApplicationDbContext _db;
//    //[HttpPost("create-order")]
//    //public IActionResult CreateOrder(decimal amount)
//    //{
//    //    RazorpayClient client =
//    //        new RazorpayClient(
//    //            "rzp_test_T2i5foOFnN8Zaj",
//    //            "yrTO7aywXy21hvR2h6ic8YCW");

//    //    Dictionary<string, object> options =
//    //        new();

//    //    options.Add(
//    //        "amount",
//    //        amount * 100);

//    //    options.Add(
//    //        "currency",
//    //        "INR");

//    //    options.Add(
//    //        "receipt",
//    //        Guid.NewGuid().ToString());

//    //    Order order =
//    //        client.Order.Create(options);

//    //    return Ok(order);
//    //}


//    [HttpPost("create-order")]
//    public IActionResult CreateOrder(decimal amount)
//    {
//        try
//        {
//            RazorpayClient client =
//                new RazorpayClient(
//               "rzp_test_T2i5foOFnN8Zaj",
//                     "yrTO7aywXy21hvR2h6ic8YCW");

//            Dictionary<string, object> options =
//                new();

//            options.Add(
//                "amount",
//                (int)(amount * 100));

//            options.Add(
//                "currency",
//                "INR");

//            options.Add(
//                "receipt",
//                Guid.NewGuid().ToString());

//            var order =
//                client.Order.Create(options);

//            return Ok(order);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(ex.Message);
//        }
//    }

//    public class PaymentRequest
//{
//    public decimal Amount { get; set; }
//}

//[HttpGet("success")]
//    public async Task<IActionResult> Success(
//    string paymentId,
//    int userId,
//    int amount)
//    {
//        var user =
//            await _db.Users.FindAsync(userId);

//        user.Coins += amount;

//        await _db.SaveChangesAsync();

//        return Ok();
//    }

//}