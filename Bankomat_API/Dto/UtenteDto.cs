﻿using Bankomat_API.Model;
using System.Text.Json.Serialization;

namespace Bankomat_API.Dto
{
    public class UtenteDto
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("Nome Utente")]
        public string NomeUtente { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("Stato")]
        public bool Bloccato { get; set; }
        [JsonPropertyName("Id Banca")]
        public long IdBanca { get; set; }
    }
}
