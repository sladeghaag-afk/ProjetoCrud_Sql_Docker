using Dapper;
using Microsoft.Data.SqlClient;
using ProjetoCrud.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoCrud.Repositories
{
    public  class PessoaRepository
    {

        private readonly string _connectionString = "";

        public void Inserir(Pessoa pessoa)
        {



            using (var connection = new SqlConnection(_connectionString))
            {


                //Executando uma instrução SQL para inserir pessoa na tabela do banco

                connection.Execute("""

                         ISERT INTO  PESSOAS(NOME, CPF, EMAIL)
                         VALUES(@Nome, @Cpf, @Email)

                    """, pessoa);



            }

        }

    }
}
