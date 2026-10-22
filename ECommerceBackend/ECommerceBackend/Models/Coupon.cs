namespace ECommerceBackend.Models
{
    public class Coupon
    {
        public string Code { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxDiscount { get; set; }
    }
}
