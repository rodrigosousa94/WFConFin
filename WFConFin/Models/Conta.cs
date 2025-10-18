using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WFConFin.Models
{
    public class Conta
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatorio")]
        [StringLength(200, ErrorMessage = "Descrição deve ter ate 200 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Valor é obrigatorio")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "DataVencimento é obrigatorio")]
        [DataType(DataType.Date, ErrorMessage = "A DataVencimento não é válida")]
        public DateTime DataVencimento { get; set; }

        [DataType(DataType.Date, ErrorMessage = "A DataPagamento não é válida")]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "Situacao é obrigatorio")]
        public Situacao Situacao { get; set; }


        public Guid PessoaId { get; set; }

        public Conta()
        {
            Id = Guid.NewGuid();
        }

        public Pessoa Pessoa { get; set; }

    }
}
