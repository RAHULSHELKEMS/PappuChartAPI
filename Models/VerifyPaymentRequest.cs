namespace PappuPictureChart.API.Models
{
    public class VerifyPaymentRequest
    {
        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentId { get; set; } = "";

        public string OrderId { get; set; } = "";
    }
}
