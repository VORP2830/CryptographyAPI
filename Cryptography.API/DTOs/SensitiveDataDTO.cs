namespace Cryptography.API.DTOs
{
    public class SensitiveDataDTO
    {
        public long Id { get; set; }
        public string UserDocument { get; set; }
        public string CreditCardToken  { get; set; }
        public double Value { get; set; }
    }
}