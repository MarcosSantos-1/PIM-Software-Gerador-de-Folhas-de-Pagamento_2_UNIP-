using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace HorizonHR
{
    public partial class Home : Form
    {
        private Pessoa pessoa = new Pessoa();

        public Home()
        {
            InitializeComponent();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // botao de minimizar
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            new Home().Show();
            this.Hide();

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

             new Funcionarios().Show();
            this.Hide();


        }
 
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            new CadastroEmpresas().Show();
            this.Close();

        }
   

        private void Home_Load(object sender, EventArgs e)
        {

        }



    }

}
