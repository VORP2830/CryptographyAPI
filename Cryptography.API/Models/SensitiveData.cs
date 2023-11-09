namespace Cryptography.API.Models
{
    public class SensitiveData
    {
        public long Id { get; set; }
        public string UserDocument { get; protected set; }
        public string CreditCardToken  { get; protected set; }
        public double Value { get; protected set; }
        public bool Active { get; protected set; } 
        protected SensitiveData() { }
        public SensitiveData(long id, string userDocument, string creditCardToken, double value)
        {
            Id = id;
            UserDocument = userDocument;
            CreditCardToken = creditCardToken;
            Value = value;
            Active = true;
        }
        public SensitiveData(string userDocument, string creditCardToken, double value)
        {
            UserDocument = userDocument;
            CreditCardToken = creditCardToken;
            Value = value;
            Active = true;
        }
        public void SetActive(bool active)
        {
            Active = active;
        }
        public void SetUserDocument(string userDocument)
        {
            UserDocument = userDocument;
        }
        public void SetCreditCardToken(string creditCardToken)
        {
            CreditCardToken = creditCardToken;
        }
    }
}