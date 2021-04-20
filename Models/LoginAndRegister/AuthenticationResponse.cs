using System.Text.Json.Serialization;

namespace ThePortal.Models.Authentication
{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string AccessToken { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefreshToken { get; set; }
    }
}
