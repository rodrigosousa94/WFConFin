using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFConFin.Models
{
    public class Pessoa
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2,ErrorMessage = "O Nome deve ter entre 2 e 200 caracteres")]
        public string Nome { get; set; }

        [StringLength(20,ErrorMessage = "O telefone deve ter ate 20 numeros")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "O email informado não é válido")] 
        public string Email { get; set; }

        [DataType(DataType.Date, ErrorMessage = "A data informada não é válida")]
        public DateTime? DataNascimento { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Salario { get; set; }

        [StringLength(20,ErrorMessage = "O campo Genero deve ter até 20 caracteres")]
        public string Genero { get; set; }

        public Guid? CidadeId { get; set; }

        public Pessoa() 
        {
            Id = Guid.NewGuid();
        }

        public Cidade Cidade { get; set; }

    }
}
