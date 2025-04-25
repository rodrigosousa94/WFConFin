using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFConFin.Models
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo 'Descricao' é obrigatório!")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo 'Descricao' deve ter entre 3 e 200 caracteres!")]
        public required string Descricao { get; set; }


        [Required(ErrorMessage = "O campo 'Valor' é obrigatório!")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }


        [Required(ErrorMessage = "O campo 'DataVencimento' é obrigatório!")]
        [DataType(DataType.Date, ErrorMessage = "O campo 'DataVencimento' deve ser uma data válida!")]
        public DateTime DataVencimento { get; set; }


        [DataType(DataType.Date, ErrorMessage = "O campo 'DataPagamento' deve ser uma data válida!")]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "O campo 'Situacao' é obrigatório!")]
        public SituacaoEnum Situacao { get; set; }


        public Guid? PessoaId { get; set; }


        public Conta()
        {
            Id = Guid.NewGuid();
        }


        public required Pessoa Pessoa { get; set; }
    }
}
