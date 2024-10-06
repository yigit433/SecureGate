using System.Text;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using SecureGate.Interfaces;
using NeoSmart.Utils;

namespace SecureGate.Services
{
    public class JwtTokenGenerator
    {
        public string GenerateToken(JwtPayload payload, string clientSecret)
        {
            var headerJson = new JsonObject();
            headerJson.Add("alg", "HS256"); 
            headerJson.Add("typ", "JWT");
            
            var headerBytes = Encoding.UTF8.GetBytes(headerJson.ToString());
            var headerBase64 = Convert.ToBase64String(headerBytes);
            
            var secretBytes = Encoding.UTF8.GetBytes(clientSecret);
            var payloadBytes = Encoding.UTF8.GetBytes(payload.ToJsonString());
            var payloadBase64 = UrlBase64.Encode(payloadBytes);
            
            var hmac = new HMACSHA256(secretBytes);
            var signatureContent = Encoding.UTF8.GetBytes(headerBase64 + "." + payloadBase64);
            var signatureBytes = hmac.ComputeHash(signatureContent);
            var signatureBase64 = UrlBase64.Encode(signatureBytes);
            
            return $"{headerBase64}.{payloadBase64}.{signatureBase64}";
        }
    }
}