using System.Text.Json.Serialization;

namespace Bankomat_API.Dto
{
    public class AdminDto
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Username")]
        public string Username { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("Id Banca")]
        public long IdBanca { get; set; }
    }
}
