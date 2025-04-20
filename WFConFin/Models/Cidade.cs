using System.ComponentModel.DataAnnotations;

namespace WFConFin.Models
{
    public class Cidade
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo 'Nome' é obrigatório!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve ter entre 3 e 200 caracteres!")]
        public required string Nome { get; set; }


        [Required(ErrorMessage = "O campo 'Estado' é obrigatório!")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "No campo 'Estado' deve ser usado apenas dois caracteres!")]
        public required string EstadoSigla { get; set; }


        public Cidade()
        {
            Id = Guid.NewGuid();
        }


        // relacionamento para o entity framework
        public required Estado Estado { get; set; }
    }
}
