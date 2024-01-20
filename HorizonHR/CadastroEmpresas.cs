using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HorizonHR
{
    public partial class CadastroEmpresas : Form
    {
        public string ObterValorTexBox()
        {
            return NomeEmpresaTxtBox.Text;
        }

        public CadastroEmpresas()
        {
            InitializeComponent();
            Cadastro.Visible = false;
            Cancel.Visible = false;
            Editar.Visible = false;
            EDITARbtn.Visible = false;
            EditarImg.Visible = false;
            excluir.Visible = false;
            ExcluirImg.Visible = false;
        


        }
        public void Cadastro_Click(object sender, EventArgs e)

        {
            var empresa = new Empresa(0, NomeEmpresaTxtBox.Text, SegmentoTxtBox.Text, CNPJMakedBox.Text, RuaTxtBox.Text, NTxtBox.Text, CEPMakedtBox.Text, UFTxtBox.Text, CidadeTxtBox.Text, BairroTxtBox.Text, ComplementoTxtBox.Text, TelefoneAMakedtBox.Text, TelefoneBMakedtBox.Text, EMailTxtBox.Text, EMailIITxtBox.Text);

            var pessoaRepositorio = new PessoaRepositorio();
            pessoaRepositorio.InserirEmpresa(empresa);
            LimparCampos();
            CarregarDadosNoDataGridView();

            MessageBox.Show("Salvo com Sucesso");

            Cadastro.Visible = false;
            Cancel.Visible = false;
            Editar.Visible = false;
            EditarImg.Visible = false;
            excluir.Visible = false;
            ExcluirImg.Visible = false;

            EDITARbtn.Visible = false;
        }
        private void Cancel_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja cancelar o cadastro?", "Cancelar Cadastro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // O usuário clicou em "Sim", então você pode cancelar a operação aqui
                LimparCampos(); 

                Cadastro.Visible = false;
                Cancel.Visible = false;
                Editar.Visible = false;
                EditarImg.Visible = false;
                excluir.Visible = false;
                ExcluirImg.Visible = false;

                EDITARbtn.Visible = false;

            }
            else if (resultado == DialogResult.No)
            {
                // O usuário clicou em "Não", então você pode continuar com a operação
                // Implemente a lógica para continuar com o cadastro aqui
            }
            else if (resultado == DialogResult.Cancel)
            {
                // O usuário clicou em "Cancelar", você pode decidir o que fazer aqui se necessário
            }
        }
      
        private void LimparCampos()
        {
            NomeEmpresaTxtBox.Text = string.Empty;
            SegmentoTxtBox.Text = string.Empty;
            CNPJMakedBox.Text = string.Empty;
            RuaTxtBox.Text = string.Empty;
            NTxtBox.Text =  string.Empty;
            CEPMakedtBox.Text = string.Empty;
            UFTxtBox.Text = string.Empty;
            CidadeTxtBox.Text = string.Empty;
            BairroTxtBox.Text = string.Empty;
            ComplementoTxtBox.Text = string.Empty;
            TelefoneAMakedtBox.Text = string.Empty;
            TelefoneBMakedtBox.Text = string.Empty;
            EMailTxtBox.Text = string.Empty;
            EMailIITxtBox.Text = string.Empty;
        }
        private void Editar_Click(object sender, EventArgs e)
        {
            Editar.Visible = true;
            EditarImg.Visible = true;
            Cancel.Visible = true;
            excluir.Visible = false;
            ExcluirImg.Visible = false;

            EDITARbtn.Visible = true;

        }


        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // botao de minimizar

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new Funcionarios().Show();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            new Home().Show();
            this.Close();
        }


    private void DataGridEmpresas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void CarregarDadosNoDataGridView()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HorizonDB;Integrated Security=True";
            string query = @"SELECT EMPRESAS.EmpresaID AS #, EMPRESAS.Nome, EMPRESAS.Segmento, EMPRESAS.CNPJ,
                               ENDERECO_EMPRESA.Rua, ENDERECO_EMPRESA.nResidencia, ENDERECO_EMPRESA.CEP,
                               ENDERECO_EMPRESA.UF, ENDERECO_EMPRESA.Cidade, ENDERECO_EMPRESA.Bairro,
                               ENDERECO_EMPRESA.Complemento, ENDERECO_EMPRESA.TelefoneA, ENDERECO_EMPRESA.TelefoneB,
                               ENDERECO_EMPRESA.eMailI, ENDERECO_EMPRESA.EmailII
                        FROM EMPRESAS
                        INNER JOIN ENDERECO_EMPRESA ON EMPRESAS.EmpresaID = ENDERECO_EMPRESA.EmpresaID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Executar a consulta SQL
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    adapter.Fill(dataTable);

                    // Associar o DataTable ao DataGridView


                    DataGridEmpresas.DataSource = dataTable;
                    DataGridEmpresas.RowHeadersVisible = false;
                    DataGridEmpresas.DataSource = dataTable;
                    DataGridEmpresas.Columns["#"].Width = 80;
                    DataGridEmpresas.Columns["Nome"].HeaderText = "Nome da Empresa";
                    DataGridEmpresas.Columns["Nome"].Width = 250;
                    DataGridEmpresas.Columns["Segmento"].Width = 250;
                    DataGridEmpresas.Columns["Rua"].Width = 250;
                    DataGridEmpresas.Columns["Cidade"].HeaderText = "Cidade";
                    DataGridEmpresas.Columns["Cidade"].Width = 150;
                    DataGridEmpresas.Columns["UF"].HeaderText = "UF";
                    DataGridEmpresas.Columns["UF"].Width = 50;
                    DataGridEmpresas.Columns["eMailI"].HeaderText = "E-mail";
                    DataGridEmpresas.Columns["eMailI"].Width = 200;
                    DataGridEmpresas.Columns["TelefoneA"].HeaderText = "Telefone";
                    DataGridEmpresas.Columns["TelefoneA"].Width = 200;
                    DataGridEmpresas.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                    DataGridEmpresas.BorderStyle = BorderStyle.None;

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void CadastroEmpresas_Load(object sender, EventArgs e)
        {
            CarregarDadosNoDataGridView();

        }

        private void DataGridEmpresas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
     
  
            Editar.Visible = true;
            EditarImg.Visible = true;
            excluir.Visible = true;
            ExcluirImg.Visible = true;



            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;


            if (dgv.CurrentRow != null)
            {
                IDEmpresaTxtbox.Text = dgv.CurrentRow.Cells["#"]?.Value?.ToString();
                NomeEmpresaTxtBox.Text = dgv.CurrentRow.Cells["Nome"]?.Value?.ToString();
                SegmentoTxtBox.Text = dgv.CurrentRow.Cells["Segmento"]?.Value?.ToString();
                CNPJMakedBox.Text = dgv.CurrentRow.Cells["CNPJ"]?.Value?.ToString();
                RuaTxtBox.Text = dgv.CurrentRow.Cells["Rua"]?.Value?.ToString();
                NTxtBox.Text = dgv.CurrentRow.Cells["nResidencia"]?.Value?.ToString();
                CEPMakedtBox.Text = dgv.CurrentRow.Cells["CEP"]?.Value?.ToString();
                UFTxtBox.Text = dgv.CurrentRow.Cells["UF"]?.Value?.ToString();
                CidadeTxtBox.Text = dgv.CurrentRow.Cells["Cidade"]?.Value?.ToString();
                BairroTxtBox.Text = dgv.CurrentRow.Cells["Bairro"]?.Value?.ToString();
                ComplementoTxtBox.Text = dgv.CurrentRow.Cells["Complemento"]?.Value?.ToString();
                TelefoneAMakedtBox.Text = dgv.CurrentRow.Cells["TelefoneA"]?.Value?.ToString();
                TelefoneBMakedtBox.Text = dgv.CurrentRow.Cells["TelefoneB"]?.Value?.ToString();
                EMailTxtBox.Text = dgv.CurrentRow.Cells["eMailI"]?.Value?.ToString();
                EMailIITxtBox.Text = dgv.CurrentRow.Cells["EmailII"]?.Value?.ToString();




            }

        }

        private void DataGridEmpresas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridEmpresas.Columns["#"].Visible = false;
            DataGridEmpresas.Columns["EmailII"].Visible = false;
            DataGridEmpresas.Columns["TelefoneB"].Visible = false;
            DataGridEmpresas.Columns["Bairro"].Visible = false;
            DataGridEmpresas.Columns["CEP"].Visible = false;
            DataGridEmpresas.Columns["nResidencia"].Visible = false;
            DataGridEmpresas.Columns["CNPJ"].Visible = false;
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Editar.Visible = true;
            EditarImg.Visible = true;
            Cancel.Visible = true;
            excluir.Visible = false;
            ExcluirImg.Visible = false;

            EDITARbtn.Visible = true;
        }

        private void EDITARbtn_Click(object sender, EventArgs e)
        {
            var empresa = new Empresa(Convert.ToInt32(IDEmpresaTxtbox.Text), NomeEmpresaTxtBox.Text, SegmentoTxtBox.Text, CNPJMakedBox.Text, RuaTxtBox.Text, NTxtBox.Text, CEPMakedtBox.Text, UFTxtBox.Text, CidadeTxtBox.Text, BairroTxtBox.Text, ComplementoTxtBox.Text, TelefoneAMakedtBox.Text, TelefoneBMakedtBox.Text, EMailTxtBox.Text, EMailIITxtBox.Text);
            var pessoaRepositorio = new PessoaRepositorio();
            pessoaRepositorio.AtualizarEmpresa(empresa);
            MessageBox.Show("Salvo com Sucesso");
            LimparCampos();
            CarregarDadosNoDataGridView();

            Cadastro.Visible = false;
            Cancel.Visible = false;
            Editar.Visible = false;
            EditarImg.Visible = false;
            excluir.Visible = false;
            ExcluirImg.Visible = false;

            EDITARbtn.Visible = false;

        }

        private void NovoCadastro_Click(object sender, EventArgs e)
        {
            Cadastro.Visible = true;
            Cancel.Visible = true;
            Editar.Visible = false;
            EditarImg.Visible = false;
            excluir.Visible = false;
            ExcluirImg.Visible = false;
            EDITARbtn.Visible = false;
            LimparCampos();


        }

        private void CadastroEmpresas_MouseClick(object sender, MouseEventArgs e)
        {
            LimparCampos();
            Cadastro.Visible = false;
            Cancel.Visible = false;
            Editar.Visible = false;
            EditarImg.Visible = false;
            excluir.Visible = false;
            ExcluirImg.Visible = false;

            EDITARbtn.Visible = false;

        }

        private void excluir_Click(object sender, EventArgs e)
        {
            {

                DialogResult resultado = MessageBox.Show("Tem certeza que deseja *Excluir* o cadastro selecionado?", "Cancelar Cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {

                    var pessoaRepositorio = new PessoaRepositorio();
                    pessoaRepositorio.DeletarEmpresa(Convert.ToInt32(IDEmpresaTxtbox.Text));
                    LimparCampos();
                    MessageBox.Show("Cadastro excluído com Sucesso!");
                    CarregarDadosNoDataGridView();
                    Cadastro.Visible = false;
                    Cancel.Visible = false;
                    Editar.Visible = false;
                    EditarImg.Visible = false;
                    excluir.Visible = false;
                    ExcluirImg.Visible = false;

                    EDITARbtn.Visible = false;

                }
                else if (resultado == DialogResult.No)
                {
                    LimparCampos();
                    Cadastro.Visible = false;
                    Cancel.Visible = false;
                    Editar.Visible = false;
                    EditarImg.Visible = false;
                    excluir.Visible = false;
                    ExcluirImg.Visible = false;

                    EDITARbtn.Visible = false;

                }


            }
        }

        private void ExcluirImg_Click(object sender, EventArgs e)

        {
            

                DialogResult resultado = MessageBox.Show("Tem certeza que deseja *Excluir* o cadastro selecionado?", "Cancelar Cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
            {

                var pessoaRepositorio = new PessoaRepositorio();
                pessoaRepositorio.DeletarEmpresa(Convert.ToInt32(IDEmpresaTxtbox.Text));
                LimparCampos();
                MessageBox.Show("Cadastro excluído com Sucesso!");
                CarregarDadosNoDataGridView();
                Cadastro.Visible = false;
                Cancel.Visible = false;
                Editar.Visible = false;
                EditarImg.Visible = false;
                excluir.Visible = false;
                ExcluirImg.Visible = false;

                EDITARbtn.Visible = false;

            }
            else if (resultado == DialogResult.No)
            {
                LimparCampos();
                Cadastro.Visible = false;
                Cancel.Visible = false;
                Editar.Visible = false;
                EditarImg.Visible = false;
                excluir.Visible = false;
                ExcluirImg.Visible = false;

                EDITARbtn.Visible = false;

            }
        }

       
    }
}
