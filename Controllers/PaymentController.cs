using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using PappuPictureChart.API.Data;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public PaymentController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost("create-order")]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            if (request == null || request.Amount <= 0)
                return BadRequest("Invalid Amount");

            var client = new RazorpayClient(
                "rzp_test_T2i5foOFnN8Zaj",
                "yrTO7aywXy21hvR2h6ic8YCW");

            Dictionary<string, object> options = new()
            {
                { "amount", (int)(request.Amount * 100) },
                { "currency", "INR" },
                { "receipt", Guid.NewGuid().ToString("N")[..20] }
            };

            Order order = client.Order.Create(options);

            return Ok(new
            {
                Success = true,
                OrderId = order["id"].ToString(),
                Amount = order["amount"].ToString(),
                Currency = order["currency"].ToString(),
                Key = "rzp_test_T2i5foOFnN8Zaj"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToString());
        }
    }
    [HttpGet("checkout")]
    public IActionResult Checkout(string orderId, decimal amount)
    {
        string html = $@"
<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<title>Pappu Picture Chart</title>
<script src='https://checkout.razorpay.com/v1/checkout.js'></script>
<style>
body {{
    margin:0;
    background:#111;
    display:flex;
    justify-content:center;
    align-items:center;
    height:100vh;
    font-family:Arial;
}}

button {{
    width:250px;
    height:55px;
    font-size:18px;
    border:none;
    border-radius:12px;
    background:#3399cc;
    color:white;
    font-weight:bold;
}}
</style>
</head>

<body>

<button id='payBtn'>Pay with UPI / GPay / PhonePe</button>

<script>

document.getElementById('payBtn').onclick = function () {{

var options = {{
    key: 'rzp_live_YOUR_KEY', // Use Live Key in production
    amount: '{(int)(amount * 100)}',
    currency: 'INR',
    name: 'Pappu Picture Chart',
    description: 'Wallet Recharge',
    image: '',
    order_id: '{orderId}',

    prefill: {{
        name: '',
        email: '',
        contact: ''
    }},

    notes: {{
        app: 'Pappu Picture Chart'
    }},

    theme: {{
        color: '#3399cc'
    }},

    retry: {{
        enabled: true,
        max_count: 3
    }},

    modal: {{
        escape: false,
        ondismiss: function(){{
            console.log('Payment Closed');
        }}
    }},

    handler: function(response){{
        alert('Payment Successful');

        window.location =
        '/payment-success?paymentId=' +
        response.razorpay_payment_id +
        '&orderId=' +
        response.razorpay_order_id;
    }}
}};

var rzp = new Razorpay(options);

rzp.on('payment.failed', function (response) {{
    alert('Payment Failed');
}});

rzp.open();

}}

</script>

</body>
</html>";

        return Content(html, "text/html");
    }

    [HttpPost("verify-payment")]
    public async Task<IActionResult> VerifyPayment(
        [FromBody] VerifyPaymentRequest request)
    {
        var user = await _db.Users.FindAsync(request.UserId);

        if (user == null)
            return BadRequest("User Not Found");

        user.Coins += request.Amount;

        await _db.SaveChangesAsync();

        return Ok(new
        {
            Message = "Coins Added Successfully",
            Coins = user.Coins
        });
    }
}

public class CreateOrderRequest
{
    public decimal Amount { get; set; }
}

public class VerifyPaymentRequest
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionId { get; set; }
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