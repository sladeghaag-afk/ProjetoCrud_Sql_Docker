using ProjetoCrud.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoCrud.Entities
{
    /// <summary>
    /// Modelo de entidade de domínio para Pessoa.
    /// </summary>
    public class Pessoa
    {
        public int Id { get; set; }

        [MinLength(6, ErrorMessage = "O nome da pessoa deve ter pelo menos {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "O nome da pessoa deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O nome da pessoa é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        [Required(ErrorMessage = "O email da pessoa é obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [CpfValidation]
        [RegularExpression("^[0-9]{11}$",ErrorMessage = "O cpf deve conter exatamente 11 números(sem pontos e traços).")]
        [Required(ErrorMessage = "O cpf da pessoa é obrigatório.")]
        public string Cpf { get; set; } = string.Empty;


        public DateTime DataHoraCadastro { get; set; } = DateTime.Now;
    }
}
