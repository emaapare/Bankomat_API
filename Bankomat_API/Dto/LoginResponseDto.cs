namespace Bankomat_API.Dto
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public UtenteDto Utente { get; set; }
        public string Message { get; set; }
    }
}
