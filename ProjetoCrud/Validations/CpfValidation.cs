using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjetoCrud.Validations
{
    /// <summary>
    /// Classe de validação customizada
    /// para validar CPF através do DataAnnotations.
    /// </summary>
    public class CpfValidation : ValidationAttribute
    {
        public CpfValidation()
        {
            ErrorMessage = "Informe um CPF válido.";
        }

        public override bool IsValid(object? value)
        {
            // Deixa o [Required] cuidar de valores vazios
            if (value == null)
                return true;

            var cpf = value.ToString();

            if (string.IsNullOrWhiteSpace(cpf))
                return true;

            // Remove pontos e traço, caso existam
            cpf = cpf
                .Replace(".", "")
                .Replace("-", "")
                .Trim();

            // CPF precisa ter exatamente 11 números
            if (cpf.Length != 11)
                return false;

            // Verifica se existem apenas números
            if (!cpf.All(char.IsDigit))
                return false;

            // Elimina CPFs com todos os números iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            /*
             * PRIMEIRO DÍGITO VERIFICADOR
             *
             * Multiplica os 9 primeiros números por:
             * 10 9 8 7 6 5 4 3 2
             */

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                int numero = int.Parse(cpf[i].ToString());
                int peso = 10 - i;

                soma += numero * peso;
            }

            int resto = soma % 11;

            int primeiroDigito =
                resto < 2 ? 0 : 11 - resto;

            if (primeiroDigito != int.Parse(cpf[9].ToString()))
                return false;

            /*
             * SEGUNDO DÍGITO VERIFICADOR
             *
             * Multiplica os 10 primeiros números por:
             * 11 10 9 8 7 6 5 4 3 2
             */

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                int numero = int.Parse(cpf[i].ToString());
                int peso = 11 - i;

                soma += numero * peso;
            }

            resto = soma % 11;

            int segundoDigito =
                resto < 2 ? 0 : 11 - resto;

            if (segundoDigito != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }
    }
}
