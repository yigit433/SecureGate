using System.Text.Json;

namespace SecureGate.Interfaces
{
    public class JwtPayload
    {
        public string Name { get; set; }
        public DateTime Exp { get; set; } 
        private DateTime Iat { get; set; }
        
        public JwtPayload(string name, int expiration)
        {
            Name = name;
            Iat = DateTime.UtcNow;
            Exp = Iat.AddMilliseconds(expiration);
        }
        
        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented  = true
            });
        }
    }
}