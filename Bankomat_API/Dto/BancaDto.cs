using Bankomat_API.Model;
using System.Text.Json.Serialization;

namespace Bankomat_API.Dto
{
    public class BancaDto
    {
        public string Nome { get; set; }

        [JsonPropertyName("Funzionalita")]
        public List<FunzionalitumDto> BancheFunzionalita { get; set; }
    }

}
