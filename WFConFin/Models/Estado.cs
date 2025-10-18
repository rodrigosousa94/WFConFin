using System.ComponentModel.DataAnnotations;

namespace WFConFin.Models
{
    public class Estado
    {
        [Key]
        [StringLength(2,MinimumLength = 2,ErrorMessage = "O campo Sigla deve ter 2 caracteres")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 200 caracteres")]
        public required string Nome { get; set; }

    }
}
