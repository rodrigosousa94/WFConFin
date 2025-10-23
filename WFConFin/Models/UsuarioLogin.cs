using System.ComponentModel.DataAnnotations;

namespace WFConFin.Models
{
    public class UsuarioLogin
    {

        [Required(ErrorMessage = "O Login é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O login deve ter entre 3 e 20 caracteres")]
        public string Login { get; set; }


        [Required(ErrorMessage = "O Password é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O Password deve ter entre 3 e 20 caracteres")]
        public string Password { get; set; }
    }
}
