using Microsoft.IdentityModel.Tokens.Experimental;
using ProjetoCrud.Entities;
using ProjetoCrud.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjetoCrud.Services
{
    /// <summary>
    /// Classe de serviço para realizar os fluxos 
    /// do sistema relacionados a entidade 'Pessoa'
    /// </summary>
    public class PessoaService
    {
        public void ExecutarMenuPrincipal()
        {
            Console.WriteLine("\nSISTEMA PARA GERENCIAMENTO DE PESSOAS:\n");
            Console.WriteLine("(1) CADASTRAR PESSOA");
            Console.WriteLine("(2) ATUALIZAR PESSOA");
            Console.WriteLine("(3) EXCLUIR PESSOA");
            Console.WriteLine("(4) CONSULTAR PESSOAS");
            Console.WriteLine("(5) SAIR");

            Console.Write("INFORME A OPÇÃO DESEJADA..: ");
            var opcao = int.Parse(Console.ReadLine() ?? string.Empty);

            switch (opcao)
            {
                case 1: CadastrarPessoa(); break;
                case 2: AtualizarPessoa(); break;
                case 3: ExcluirPessoa(); break;
                case 4: ConsultarPessoas(); break;
                case 5:
                    Console.WriteLine("\nFIM DO PROGRAMA!");
                    break;
                default:
                    Console.WriteLine("\nOPÇÃO INVÁLIDA!");
                    break;
            }

            Console.WriteLine("\nPRESSIONE UMA TECLA PARA CONTINUAR...");
            Console.ReadKey();

            if (opcao != 5)
            {
                Console.Clear(); //limpar o console
                ExecutarMenuPrincipal(); //recursividade
            }
        }

        private void CadastrarPessoa()
        {
            Console.WriteLine("\nCADASTRAR PESSOA:\n");

            var pessoa = new Pessoa();

            Console.Write("INFORME O NOME.........: ");
            pessoa.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("INFORME O EMAIL........: ");
            pessoa.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("INFORME O CPF..........: ");
            pessoa.Cpf = Console.ReadLine() ?? string.Empty;

            if (ValidarPessoa(pessoa))
            {
                var pessoaRepository = new PessoaRepository();
                pessoaRepository.Inserir(pessoa);

                Console.WriteLine("\n PESSOA CADASTRADA COM SUCESSO!");



            }



        }

        

        private void AtualizarPessoa()
        {
            Console.WriteLine("\nATUALIZAR PESSOA:\n");

            Console.Write("INFORME O ID DA PESSOA.: ");
            var id = int.Parse(Console.ReadLine() ?? string.Empty);

            var pessoaRepository = new PessoaRepository();
            var pessoa = pessoaRepository.ObterPorId(id);

            if (pessoa != null)
            {
                Console.WriteLine("\nDADOS DA PESSOA:");
                Console.WriteLine("\tNOME....: " + pessoa.Nome);
                Console.WriteLine("\tEMAIL...: " + pessoa.Email);
                Console.WriteLine("\tCPF.....: " + pessoa.Cpf);

                Console.WriteLine("\nINFORME OS NOVOS DADOS:\n");

                Console.Write("INFORME O NOME.........: ");
                pessoa.Nome = Console.ReadLine() ?? string.Empty;

                Console.Write("INFORME O EMAIL........: ");
                pessoa.Email = Console.ReadLine() ?? string.Empty;

                Console.Write("INFORME O CPF..........: ");
                pessoa.Cpf = Console.ReadLine() ?? string.Empty;

                if (ValidarPessoa(pessoa)) 
                {

                    pessoaRepository.Atualizar(pessoa);

                    Console.WriteLine("\nPESSOA ATUALIZAR COM SUCESSO!");
                
                }

            }
            else
            {
                Console.WriteLine("\nPESSOA NÃO ENCONTRADA.");
            }
        }

        private void ExcluirPessoa()
        {
            Console.WriteLine("\n EXCLUIR PESSOA: \n");

            Console.Write("INFORME O ID DA PESSOA.: ");
            var id = int.Parse(Console.ReadLine() ?? string.Empty);

            var pessoaRepository = new PessoaRepository();
            var pessoa = pessoaRepository.ObterPorId(id);


            if(pessoa != null)
            {

                Console.WriteLine("\nDADOS DA PESSOA:");
                Console.WriteLine("\tNOME....: " + pessoa.Nome);
                Console.WriteLine("\tEMAIL...: " + pessoa.Email);
                Console.WriteLine("\tCPF.....: " + pessoa.Cpf);

                Console.Write("\n DESEJA REALMENTE EXCLUIR? (S,N): ");
                var opcao = Console.ReadLine() ?? string.Empty;

                if (opcao.Equals("S", StringComparison.OrdinalIgnoreCase)) 
                {

                    pessoaRepository.Excluir(id);

                    Console.WriteLine("\nPESSOA EXCLUÍDA COM SUCESSO.");

             
                }


            }
            else
            {


                Console.WriteLine("\nPESSOA NÃO ENCONTRADA");

            }




        }

        private void ConsultarPessoas()
        {

            Console.WriteLine("\nCONSULTA DE PESSOA: \n");

            var pessoaRepository = new PessoaRepository();

            var pessoas = pessoaRepository.ObterTodos();

            foreach (var pessoa in pessoas)
            {
                Console.WriteLine($"ID: {pessoa.Id}, NOME: {pessoa.Nome}, CPF: {pessoa.Cpf}, EMAIL: {pessoa.Email}");

            }

        }

        private bool ValidarPessoa (Pessoa pessoa) 
        {

            #region Executar as regras de validação (Data Annotations)

            var validation = new ValidationContext(pessoa);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(
                    pessoa,
                    validation,
                    errors,
                    validateAllProperties: true
                );
            
            if (! isValid)
            {
                foreach (var item in errors)
                {
                    Console.WriteLine("\tERRO: " + item.ErrorMessage);
                }
            }

            var pessoaRepository = new PessoaRepository();
            if(pessoaRepository.VerificarCpf(pessoa.Cpf, pessoa.Id))
            {

                Console.WriteLine("ESTE CPF JÁ ESTÁ CADASTRADO PARA OUTRA PESSOA: ");
                Console.WriteLine("\tCPF: " + pessoa.Cpf);
                Console.WriteLine("NÃO É POSSIVEL CONCLUIR ESTA OPERAÇÃO.");


                isValid = false;

            }


            return isValid;

            #endregion



        }


    }
}
