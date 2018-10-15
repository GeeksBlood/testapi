using System.ComponentModel.DataAnnotations;

namespace Aguila.Models.AccountModel
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
