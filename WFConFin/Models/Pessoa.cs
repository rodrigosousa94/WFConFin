using System.ComponentModel.DataAnnotations;
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


        [StringLength(20, MinimumLength = 8, ErrorMessage = "O campo 'Telefone' deve ter entre 8 e 20 caracteres!")]
        public string? Telefone { get; set; }


        [StringLength(200, MinimumLength = 5, ErrorMessage = "O campo 'Email' deve ter entre 5 e 200 caracteres!")]
        [EmailAddress(ErrorMessage = "O campo 'Email' deve ser um endereço de e-mail válido!")]
        public string? Email { get; set; }


        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo 'DataNascimento' deve ter entre 3 e 20 caracteres!")]
        [DataType(DataType.Date, ErrorMessage = "O campo 'DataNascimento' deve ser uma data válida!")]
        public DateTime DataNascimento { get; set; }


        
        public decimal Salario { get; set; }


        [StringLength(20, MinimumLength = 3, ErrorMessage = "O campo 'Genero' deve ter entre 3 e 20 caracteres!")]
        public string? Genero { get; set; }


        [Required(ErrorMessage = "O campo 'Cidade' é obrigatório!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo 'Cidade' deve ter entre 3 e 200 caracteres!")]
        public required string CidadeId { get; set; }




        public required Cidade Cidade { get; set; }




    }
}
