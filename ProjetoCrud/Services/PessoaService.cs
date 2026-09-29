using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjetoCrud.Services
{
    public class PessoaService
    {

        public void ExecutarMenuPrincipal()
        {

            Console.WriteLine("\n SISTEMA PARA GERENCIAMENTO DE PESSOAS:\n");
            Console.WriteLine("(1) CADASTRAR PESSOA");
            Console.WriteLine("(2) ATUALIZAR PESSOA");
            Console.WriteLine("(3) EXCLUIR PESSOA");
            Console.WriteLine("(4) CONSULTAR PESSOA");
            Console.WriteLine("(5) SAIR");


            Console.Write("INFORME A OPÇÃO DESEJADA..: ");


            var opcao = int.Parse(Console.ReadLine() ?? string.Empty); 

            switch(opcao)
            {

                case 1: CadastrarPessoa(); break;
                case 2: AtualizarPessoa();break;
                case 3: ExcluirPessoa(); break;
                case 4: ConsultarPessoas(); break;
                case 5:
                    Console.WriteLine("\n FIM DO PROGRAMA!");break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;

             

            }

            Console.WriteLine("\nPRESSIONE UMA TECLA PARA CONTINUAR");
            Console.ReadKey();


            if(opcao != 5)
            {

                Console.Clear();
                ExecutarMenuPrincipal();

            }

        }


        private void CadastrarPessoa()
        {








        }

        private void AtualizarPessoa()
        {





        }
        private void ExcluirPessoa() 
        {
        
        



        
        
        }
        private void ConsultarPessoas()
        {







        }




    }
}
