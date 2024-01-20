using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
using System.Data.SqlClient;

namespace HorizonHR
{
    public partial class TelaLogin : Form
    {
        //Referencia de Conexão
        SqlConnection connection = new SqlConnection(@"Integrated Security =SSPI; Persist Security Info=False; Initial Catalog= HorizonDB ;Data Source=(localdb)\MSSQLLocalDB");
        public TelaLogin()
        {
            InitializeComponent();
            
        }
        private void TelaLogin_Load(object sender, EventArgs e)
        {  }
        private void txtuser_TextChanged(object sender, EventArgs e)
        {  }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {  }

        void Verificar()
        {
            if (txtuser.Text.Trim() == "" && txtpassword.Text.Trim() == "")
            {
                MessageBox.Show(" Preencha os campos: ");
            }
            else if (txtuser.Text.Trim() == "")
            {
                MessageBox.Show(" Informe o nome de usuário: ");
            }
            else if (txtpassword.Text.Trim() == "")
            {
                MessageBox.Show(" Informe a senha: ");
            }
           
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            //APAGAR ESSA PARTE QUANDO O SOFTWARE TIVER TERMINADO
            Home principal = new Home();
           


            connection.Open(); //Abrir a conexao
            Verificar();
            string query = "SELECT * FROM USERLOGIN WHERE Usuario = '" + txtuser.Text + "' AND Senha = '" +txtpassword.Text + " '";
            SqlDataAdapter dp = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            dp.Fill(dt);
            try
            {
                if (dt.Rows.Count == 1) // Credenciais válidas
                {
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nome de usuário ou senha incorretos.");
                    // Limpar os campos e focar no campo de usuário
                    txtuser.Clear();
                    txtpassword.Clear();
                    txtuser.Focus();
                }

            }


            catch(Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
                txtuser.Clear();
                txtpassword.Clear();
                txtuser.Focus();
            }

            connection.Close(); //FECHAR CONEXAO
            
        }



        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
