namespace SecureGate.Interfaces
{
    public class ILoginPayload
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}