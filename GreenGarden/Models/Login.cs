using Microsoft.Data.SqlClient;

namespace GreenGarden.Models
{
    public class Login
    {
        string strLog = "SELECT * FROM [dbo].[cliente] WHERE email COLLATE SQL_Latin1_General_CP1_CS_AS = @email AND senha COLLATE SQL_Latin1_General_CP1_CS_AS = @senha";
        
        Cadastro cad = new Cadastro();



        public string Email { get; set; }
        public string Password { get; set; }
        public bool logScss = false;

        public void logar()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cad.strcon))
                {
                    con.Open();

                    using (SqlCommand cmdLog = new SqlCommand(strLog, con))
                    {
                        cmdLog.Parameters.AddWithValue("@email", Email);
                        cmdLog.Parameters.AddWithValue("@senha", Password);

                        using (SqlDataReader reader = cmdLog.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine("Usuario logado com sucesso");
                                logScss = true;
                            }
                            else
                            {
                                Console.WriteLine("Usuario ou senha estão incorretos");
                                return;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
