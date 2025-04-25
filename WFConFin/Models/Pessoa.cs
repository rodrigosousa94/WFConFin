using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WFConFin.Models
{
    public class Pessoa
    {

        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo 'Nome' é obrigatório!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo 'Nome' deve ter entre 3 e 200 caracteres!")]
        public required string Nome { get; set; }


        [StringLength(20, ErrorMessage = "O campo 'Telefone' deve ter até 20 caracteres!")]
        public string? Telefone { get; set; }


        [EmailAddress(ErrorMessage = "O campo 'Email' deve ser um endereço de e-mail válido!")]
        public string? Email { get; set; }


        [DataType(DataType.Date, ErrorMessage = "O campo 'DataNascimento' deve ser uma data válida!")]
        public DateTime? DataNascimento { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Salario { get; set; }


        [StringLength(20, ErrorMessage = "O campo 'Genero' deve ter até e 20 caracteres!")]
        public string? Genero { get; set; }


        public Guid? CidadeId { get; set; }


        public Pessoa()
        {
            Id = Guid.NewGuid();
        }   


        public required Cidade Cidade { get; set; }




    }
}
