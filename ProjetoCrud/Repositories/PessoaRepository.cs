using Dapper;
using Microsoft.Data.SqlClient;
using ProjetoCrud.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCrud.Repositories
{
    /// <summary>
    /// Repositório para inserir, alterar, excluir e consultar
    /// dados de pessoas no banco de dados
    /// </summary>
    public class PessoaRepository
    {
        private readonly string _connectionString = "Server=localhost,1434; Database=master; User Id=sa; Password=Coti@2026; TrustServerCertificate=True";

        public void Inserir(Pessoa pessoa)
        {
            //Abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                //Executando uma instrução SQL para inserir pessoa na tabela do banco
                connection.Execute("""
                        INSERT INTO PESSOAS(NOME, CPF, EMAIL) 
                        VALUES(@Nome, @Cpf, @Email)
                    """, pessoa);
            }
        }

        public void Atualizar(Pessoa pessoa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                        UPDATE PESSOAS 
                        SET
                            NOME = @Nome,
                            CPF = @Cpf,
                            EMAIL = @Email
                        WHERE
                            ID = @Id
                    """, pessoa);
            }
        }

        public void Excluir(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                        DELETE FROM PESSOAS
                        WHERE ID = @Id
                    """, new { @Id = id });
            }
        }

        public List<Pessoa> ObterTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Pessoa>("""
                        SELECT ID, NOME, EMAIL, CPF, DATAHORACADASTRO
                        FROM PESSOAS
                        ORDER BY ID
                    """).ToList();
            }
    
  
        }


        public Pessoa? ObterPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                return connection.QuerySingleOrDefault<Pessoa>("""
                       
                       SELECT ID, NOME, EMAIL, CPF, DATAHORACADASTRO
                       FROM PESSOAS
                       WHERE ID = @Id


                  """, new {@Id = id});
  
            
            }



        }

        public bool VerificarCpf(string cpf, int id = 0)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {

                var qtd = connection.QuerySingle<int>("""

                           SELECT COUNT(*)
                           FROM PESSOAS
                           WHERE CPF = @Cpf
                           AND ID <> @Id


                    """,new
                {
                  @Cpf = cpf,
                  @Id = id
                });


                return qtd > 0; 
            
            }

        }


    }

}

