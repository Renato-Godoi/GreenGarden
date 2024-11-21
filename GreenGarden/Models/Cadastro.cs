using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using System.Data;
using Microsoft.Data.SqlClient;




namespace GreenGarden.Models
{
    public class Cadastro
    {

        string strcon = @"Data Source=ATHIRSON-GAMER;" +
                       "Initial Catalog=GreenGardenDB;Integrated Security=True; TrustServerCertificate=True";

        string strcadastrar = "INSERT INTO [dbo].[cliente] (email, cliente_nome, cpf, nome_usuario, senha)" +
            "VALUES (@email, @cliente_nome, @cpf, @nome_usuario, @senha)";

        string strVer = " SELECT COUNT(*) FROM [dbo].[Funcionario] WHERE nome_usuario  COLLATE SQL_Latin1_General_CP1_CS_AS = @nome";

        string dfnEnd = "INSERT INTO [dbo].[Endereco] (cep,cidade,bairro,rua,numero) " +
            "VALUES (@cep ,@cidade ,@bairro ,@rua ,@numero)";

        string strLog = "SELECT * FROM [dbo].[Funcionario] WHERE nome_usuario COLLATE SQL_Latin1_General_CP1_CS_AS = @nome AND senha COLLATE SQL_Latin1_General_CP1_CS_AS = @senha";
        //string strVerSenha = "SELECT * FROM [dbo].[USUARIOS] WHERE senha = @senha";

        private string _name;
        private string _senha;
        private string _senhaC;

        private string nome_completo = "teste";

        [EmailAddress]
        private string email;
        private string tel;
        private string cpf;

        
        private Endereco endereco;


        public bool _estLgd = false; //LOGIN BEM SUCEDIDO OU MAL SUCEDIDO
        public bool _cadScss = false; // CAD BEM SUCEDIDO OU MAL SUCEDIDO
        private string erro;

        public string Name { get => _name; set => _name = value; }
        public string Senha { get => _senha; set => _senha = value; }
        public string SenhaC { get => _senhaC; set => _senhaC = value; }
        public string Email { get => email; set => email = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        //public Endereco Endereco { get => endereco; set => endereco = value; }
        public string Nome_completo { get => nome_completo; set => nome_completo = value; }

        public void Cadastrar()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    if (SenhaC != Senha)
                    {
                        Console.WriteLine("Senhas divergentes");
                        return;
                    }
                    using (SqlCommand cmd = new SqlCommand(strcadastrar, con))
                    {
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@cliente_nome", Nome_completo);
                        cmd.Parameters.AddWithValue("@cpf", Cpf);
                        cmd.Parameters.AddWithValue("@nome_usuario", Name);
                        cmd.Parameters.AddWithValue("@senha", Senha);

                        int linhasafetada = cmd.ExecuteNonQuery();
                        if (linhasafetada > 0)
                        {
                            _cadScss = true;
                            Console.WriteLine("Cadastro bem sucedido");
                        }
                        else
                        {
                            Console.WriteLine("Cadastro falhou");
                            return ;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                erro = ex.Message;
                Console.WriteLine(erro);
            }
        }
    }

    
}
