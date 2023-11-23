using System.ComponentModel.DataAnnotations;

namespace Bankomat_API.Model
{
    public class Admin
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Il campo Id Banca è obbligatorio")]
        public long IdBanca { get; set; }

        [Required(ErrorMessage = "Il campo Id Username è obbligatorio")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Il campo Password è obbligatorio")]
        public string Password { get; set; } = null!;

        public virtual Banche IdBancaNavigation { get; set; } = null!;
    }
}
