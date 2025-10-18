﻿using System.ComponentModel.DataAnnotations;

namespace WFConFin.Models
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Nome é requirido")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O Nome deve ter entre 3 e 200 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Login é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O login deve ter entre 3 e 20 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O Password é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O Password deve ter entre 3 e 20 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "A funcao é obrigatório")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O funcao deve ter entre 3 e 20 caracteres")]
        public string Funcao { get; set; }


        public Usuario()
        { 
            Id = Guid.NewGuid();
        }
    }
}
