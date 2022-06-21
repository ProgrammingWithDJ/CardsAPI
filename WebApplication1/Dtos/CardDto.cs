namespace WebApplication1.Dtos
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string CardholderName { get; set; }

        public int CardNumber { get; set; }

        public int CVV { get; set; }
        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
