using System.ComponentModel.DataAnnotations;

namespace WFConFin.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo 'Nome' é obrigatório!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve ter entre 3 e 200 caracteres!")]
        public required string Nome { get; set; }


        [Required(ErrorMessage = "O campo 'Login' é obrigatório!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo 'Login' deve ter entre 3 e 20 caracteres!")]
        public required string Login { get; set; }


        [Required(ErrorMessage = "O campo 'Password' é obrigatório!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo 'Password' deve ter entre 6 e 20 caracteres!")]
        public required string Password { get; set; }


        [Required(ErrorMessage = "O campo 'Funcao' é obrigatório!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo 'Funcao' deve ter entre 3 e 20 caracteres!")]
        public required string Funcao { get; set; } 
    }
}
