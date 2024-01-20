using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;
using Microsoft.OData.Edm;
using HorizonHR.Repositorio;
using Dapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.Kernel.Colors;

namespace HorizonHR
{
    public partial class Funcionarios : Form
    {

        public List<Empresa> ObterEmpresasDoBancoDeDados()   //CÓDIGO PARA OBTER OS DADOS DAS EMPRESAS PARA UTILIZAR NO COMBO BOX
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            {
                return connection.Query<Empresa>("SELECT * FROM EMPRESAS").ToList();
            }
        }

        public Funcionarios( )
        {
            InitializeComponent();
            //COMBO BOX:
            comboBoxEmpresas.DataSource = ObterEmpresasDoBancoDeDados();
            comboBoxEmpresas.DisplayMember = "Nome"; // Ou o nome da propriedade que deseja exibir
            comboBoxEmpresas.ValueMember = "EmpresaID";

            //DESATIVANDO E CONFIGURANDO A ENTRADA NO FORMULRÍO:
            BeneficioITxtBox.Enabled = false;
            ValorITxtBox.Enabled = false;
            ChkSimVT.Checked = true;
            SalvarButton.Visible = false;
            CancelarButton.Visible = false;
            EditarButton.Visible = false;
            EditImg.Visible = false;
            ExcluirButtton.Visible = false;
            ExcluirImg.Visible = false;
            EDITARbtn.Visible = false;

        }

        //BOTAO DE SAIR (FECHAR NO X)
        private void FecharJanela_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //BOTAO DE MINIMIZAR
        private void Minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //DADOS DEFAULT DOS CHECKBOX SEXO:
        char sexo = 'U';//Valor fica como o padrao (Indefinido)
        char Beneficio = 'N'; //Valor fica como o padrao (nÃO)
        char VT = 'U';//Valor fica como o padrao (Indefinido)

        //VARIAVEIS NUMÉRICAS (PARA CONVERTÊ-LAS EM STRING)
        decimal salario;
        int cargaHoraria;
        decimal valorI;
        decimal valorVT;

        //ALGORITMO DO CHECKBOX MASCULINO:
        private void chkMasculino_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMasculino.Checked)
            {
                // Se o checkbox "Masculino" estiver marcado, desmarque os outros e desabilite os checkboxes e habilitar o TextBox.
                chkFeminino.Checked = false;
                chkOutros.Checked = false;
                txtOutros.Enabled = false;
                SexualidadOutrosTxtBox.Enabled = false;
                SexualidadOutrosTxtBox.Text = "";
            }
            else
            {
                SexualidadOutrosTxtBox.Enabled = true;
                txtOutros.Enabled = true;
            }
        }

        //ALGORITMO DO CHECKBOX FEMININO:
        private void chkFeminino_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFeminino.Checked)
            {
                // Se o CheckBox "Feminino" estiver marcado, desmarcar os outros CheckBoxes.
                chkMasculino.Checked = false;
                chkOutros.Checked = false;
                txtOutros.Enabled = false;
                SexualidadOutrosTxtBox.Enabled = false;
                SexualidadOutrosTxtBox.Text = "";
            }
            else
            {
                SexualidadOutrosTxtBox.Enabled = true;
                txtOutros.Enabled = true;
            }
        }

        //ALGORITMO DO CHECKBOX OUTROSSEXO:
        private void chkOutros_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOutros.Checked)
            {
                // Se o CheckBox "Outros" estiver marcado, desmarcar os outros CheckBoxes.
                chkMasculino.Checked = false;
                chkFeminino.Checked = false;
                SexualidadOutrosTxtBox.Enabled = true;
                txtOutros.Enabled = true;
            }
        }

        //ALGORITMO DO CHECKBOX BENEFICIO:

        private void ChkBeneficioI_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBeneficioI.Checked)
            {
                BeneficioITxtBox.Enabled = true;
                ValorITxtBox.Enabled = true;
            }
            else
            {
                BeneficioITxtBox.Enabled = false;
                ValorITxtBox.Enabled = false;
                BeneficioITxtBox.Text = "";
                ValorITxtBox.Text = "";
            }
        }

        //ALGORITMO DO CHECKBOX NÃO VT:
        private void ChkNaoVT_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkNaoVT.Checked)
            {
                ValorVTTxtBox.Enabled = false;
                ValorVTTxtBox.Text = "";
                ChkSimVT.Checked = false;
                label32.Enabled= false;
            }
            else
            {
                ValorVTTxtBox.Enabled = true;
                ChkSimVT.Checked = true;
            }
        }

        //ALGORITMO DO CHECKBOX SIM VT:
        private void ChkSimVT_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSimVT.Checked)
            {
                ChkNaoVT.Checked = false;
               label32.Enabled = true;

            }
        }

        //ALGORITMO DO CHECKBOX NÃO VT:
        private void Cadastro_Click(object sender, EventArgs e)
        {
            LimparCampos();

            if (comboBoxEmpresas.SelectedItem != null)
            {
                int empresaID = (int)comboBoxEmpresas.SelectedValue; //ALOCANDO UMA VARIÁVEL PARA RECEBER E EXIBIR O COMBOBOX

                //CONDIICONAIS DAS CHECKBOXç
                if (chkMasculino.Checked)
                {
                    sexo = 'M';
                }
                else if (chkFeminino.Checked)
                {
                    sexo = 'F';
                }
                else if (chkOutros.Checked)
                {
                    sexo = 'O';

                }
                if (ChkBeneficioI.Checked)
                {
                    Beneficio = 'S';
                }
                if (ChkSimVT.Checked)
                {
                    VT = 'S';
                }
                if (ChkNaoVT.Checked)
                {
                    VT = 'N';
                }
                //CONVERTENDO OS VALORES NUMERICOS PARA A SAIDA NO BD
                if (decimal.TryParse(SalarioBTxtBox.Text, out salario) &&
                int.TryParse(CargaHorariaTxtBox.Text, out cargaHoraria) &&
                decimal.TryParse(ValorITxtBox.Text, out valorI) &&
                 decimal.TryParse(ValorVTTxtBox.Text, out valorVT))
                {   
                }

                //CRIANDO UM NOVO OBJETO DA CLASSE CONSTRUTORA NO PessoaRepositorio
                var pessoa = new Pessoa(0, empresaID, NomeTxtBox.Text, CPFMakedtBox.Text, NascimentoBox.Text, sexo, SexualidadOutrosTxtBox.Text, RuaTxtBox.Text, NTxtBox.Text,
                              CEPMakedtBox.Text, UFTxtBox.Text, CidadeTxtBox.Text, TelefoneAMakedtBox.Text, EMailTxtBox.Text, salario, TipodeContratoTxtBox.Text, Departamento.Text,  CargoTxtBox.Text, 
                              cargaHoraria, DtAdmissaoBox.Text, Beneficio, BeneficioITxtBox.Text, valorI, VT, valorVT);
                
                //INSTANCIANDO O NOVO OBJETO
                var pessoaRepositorio = new PessoaRepositorio();

                //INSERINDO O OBJETO NO BD (CREATE)
                pessoaRepositorio.Inserir(pessoa);
               MessageBox.Show("Salvo com Sucesso");
                LimparCampos();
                CarregarDadosNoDataGridView();


                SalvarButton.Visible = false;
                CancelarButton.Visible = false;
                EditarButton.Visible = false;
                EditImg.Visible = false;
                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;
                EDITARbtn.Visible = false;
            }
        }

        //ALGORITMO DO BOTAO DE CANCELAR CADASTRO
        private void Cancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Tem certeza que deseja cancelar o cadastro?", "Cancelar Cadastro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                LimparCampos();
                NovoCadastro.Visible = true;
                CancelarButton.Visible = false;
                EditarButton.Visible = false;
                EditImg.Visible = false;
                SalvarButton.Visible = false;
                EDITARbtn.Visible = false;
                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;

            }
            else if (resultado == DialogResult.No)
            {
            }
            else if (resultado == DialogResult.Cancel)
            {
            }
        }

        //CARREGA O DATA GRID COM BASE NO BANCO DE DADOS
        public void CarregarDadosNoDataGridView()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HorizonDB;Integrated Security=True";
            string query = @"
    SELECT F.*, E.Nome AS NomeEmpresa, E.CNPJ
    FROM FUNCIONARIOS F
    INNER JOIN EMPRESAS E ON F.EmpresaID = E.EmpresaID;";

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

                    DataGridEmpresas.Columns["FuncionarioID"].HeaderText = "#";
                    DataGridEmpresas.Columns["FuncionarioID"].Width = 80;

                    DataGridEmpresas.Columns["NomeEmpresa"].HeaderText = "Empresa";
                    DataGridEmpresas.Columns["NomeEmpresa"].Width = 250;
                    DataGridEmpresas.Columns["CNPJ"].HeaderText = "CNPJ";
                    DataGridEmpresas.Columns["Nome"].HeaderText = "Nome Completo";
                    DataGridEmpresas.Columns["Nome"].Width = 280;
                    DataGridEmpresas.Columns["Departamento"].HeaderText = "Departamento";
                    DataGridEmpresas.Columns["Departamento"].Width = 150;
                    DataGridEmpresas.Columns["Cargo"].HeaderText = "Cargo";
                    DataGridEmpresas.Columns["Cargo"].Width = 200;
                    DataGridEmpresas.Columns["eMail"].HeaderText = "E-mail";
                    DataGridEmpresas.Columns["eMail"].Width = 230;
                    DataGridEmpresas.Columns["TelefoneA"].HeaderText = "Contato";
                    DataGridEmpresas.Columns["TelefoneA"].Width = 150;
                    DataGridEmpresas.Columns["Cargo"].Width = 180;
                    DataGridEmpresas.Columns["DataAdmissao"].Width = 180;
                    DataGridEmpresas.Columns["DataAdmissao"].HeaderText = "Data de Admissao";
                    DataGridEmpresas.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                    DataGridEmpresas.BorderStyle = BorderStyle.None;
              
                    DataGridEmpresas.Columns["FuncionarioID"].DisplayIndex = 0;
                    DataGridEmpresas.Columns["Nome"].DisplayIndex = 1;  // Coluna existente "Nome"
                    DataGridEmpresas.Columns["NomeEmpresa"].DisplayIndex = 2;  // Nova coluna "Empresa"
                    DataGridEmpresas.Columns["Departamento"].DisplayIndex = 3;
                    DataGridEmpresas.Columns["Cargo"].DisplayIndex = 4;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        //CARREGA O DATA GRID QUANDO INICIAR O FORMULARIO:
        private void Funcionarios_Load(object sender, EventArgs e)
        {
            CarregarDadosNoDataGridView();
        }

        //DESABILITANDO AS COLUNAS DESNECESSARIAS DO DATA GRID
        private void DataGridEmpresas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridEmpresas.Columns["CNPJ"].Visible = false;
            DataGridEmpresas.Columns["CPF"].Visible = false;
            DataGridEmpresas.Columns["EmpresaID"].Visible = false;
            DataGridEmpresas.Columns["DataNascimento"].Visible = false;
            DataGridEmpresas.Columns["Sexo"].Visible = false;
            DataGridEmpresas.Columns["OutroSexo"].Visible = false;
            DataGridEmpresas.Columns["Rua"].Visible = false;
            DataGridEmpresas.Columns["nResidencia"].Visible = false;
            DataGridEmpresas.Columns["Cidade"].Visible = false;
            DataGridEmpresas.Columns["CEP"].Visible = false;
            DataGridEmpresas.Columns["UF"].Visible = false;
            DataGridEmpresas.Columns["SalarioBase"].Visible = false;
            DataGridEmpresas.Columns["TipoContrato"].Visible = false;
            DataGridEmpresas.Columns["HorasTrabalho"].Visible = false;
            DataGridEmpresas.Columns["OutrosBeneficios"].Visible = false;
            DataGridEmpresas.Columns["NomeBeneficiosI"].Visible = false;
            DataGridEmpresas.Columns["ValorBeneficioI"].Visible = false;
            DataGridEmpresas.Columns["VT"].Visible = false;
            DataGridEmpresas.Columns["ValorVT"].Visible = false;
  
        }

        //CAMPO DE PESQUISA DE FUNCIONARIOS
        private void Pesquisarbtn_Click(object sender, EventArgs e)
        {
            string termoBusca = PesquisaBar.Text;
            string consultaSql = "SELECT * FROM FUNCIONARIOS WHERE Nome LIKE @TermoBusca OR Email LIKE @TermoBusca ";

            using (SqlConnection conexao = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HorizonDB;Integrated Security=True"))
                try
            {
                using (SqlCommand comando = new SqlCommand(consultaSql, conexao))
                {
                    conexao.Open();
                    // Adiciona o parâmetro com LIKE à consulta SQL
                    comando.Parameters.AddWithValue("@TermoBusca", "%" + termoBusca + "%");
                    DataTable resultado = new DataTable();
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(resultado);
                    DataGridEmpresas.DataSource = resultado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }


        //IR PARA TELA DE CADASTRO DE EMPRESAS:
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            new CadastroEmpresas().Show();
            this.Hide();
        }

        //IR PARA TELA DE HOME:
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            new Home().Show();
            this.Close();
        }


        //FUNÇÃO DE LIMPAR OS TEXT BOXES:
        private void LimparCampos()
        {
            IDtext.Text = string.Empty;
            comboBoxEmpresas.SelectedIndex = -1; // Limpa a seleção do ComboBox
            NomeTxtBox.Text = string.Empty;
            CPFMakedtBox.Text = string.Empty;
            NascimentoBox.Text = string.Empty;
            // Limpar outros campos de texto
            RuaTxtBox.Text = string.Empty;
            NTxtBox.Text = string.Empty;
            CEPMakedtBox.Text = string.Empty;
            UFTxtBox.Text = string.Empty;
            CidadeTxtBox.Text = string.Empty;
            TelefoneAMakedtBox.Text = string.Empty;
            EMailTxtBox.Text = string.Empty;
            SalarioBTxtBox.Text = string.Empty;
            TipodeContratoTxtBox.Text = string.Empty;
            Departamento.Text = string.Empty;
            CargoTxtBox.Text = string.Empty;
            CargaHorariaTxtBox.Text = string.Empty;
            DtAdmissaoBox.Text = string.Empty;
            BeneficioITxtBox.Text = string.Empty;
            ValorITxtBox.Text = "00,00";
            ValorVTTxtBox.Text = "7,0"; 
            // Limpar CheckBoxes
            chkMasculino.Checked = false;
            chkFeminino.Checked = false;
            chkOutros.Checked = false;
            ChkBeneficioI.Checked = false;
            ChkSimVT.Checked = false;
            ChkNaoVT.Checked = false;
        }


        //QUANDO CLICAR EM UMA CELULA DO DATA GRID, CARREGA OS DADOS NOS TEXTBOX DO RESPECTIVO FUNCIONARIO CLICADO (READ):
        private void DataGridEmpresas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                    DataGridView dgv = sender as DataGridView;
                    if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                    {
                        DataGridViewRow row = dgv.Rows[e.RowIndex];
                        // Preencher os campos de texto e elementos com base nas colunas do banco de dados
                        IDtext.Text = row.Cells["FuncionarioID"].Value?.ToString();
                        comboBoxEmpresas.Text = row.Cells["NomeEmpresa"].Value?.ToString();
                        NomeTxtBox.Text = row.Cells["Nome"].Value?.ToString();
                        CPFMakedtBox.Text = row.Cells["CPF"].Value?.ToString();
                        NascimentoBox.Text = row.Cells["DataNascimento"].Value?.ToString();
                        char sexo = row.Cells["Sexo"].Value?.ToString()[0] ?? '\0'; // Obtendo o char do DataGridView
                            chkMasculino.Checked = sexo == 'M';
                            chkFeminino.Checked = sexo == 'F';
                            chkOutros.Checked = sexo == 'O';
                        SexualidadOutrosTxtBox.Text = row.Cells["OutroSexo"].Value?.ToString();
                        RuaTxtBox.Text = row.Cells["Rua"].Value?.ToString();
                        NTxtBox.Text = row.Cells["nResidencia"].Value?.ToString();
                        CEPMakedtBox.Text = row.Cells["CEP"].Value?.ToString();
                        UFTxtBox.Text = row.Cells["UF"].Value?.ToString();
                        CidadeTxtBox.Text = row.Cells["Cidade"].Value?.ToString();
                        TelefoneAMakedtBox.Text = row.Cells["TelefoneA"].Value?.ToString();
                        EMailTxtBox.Text = row.Cells["eMail"].Value?.ToString();
                        SalarioBTxtBox.Text = row.Cells["SalarioBase"].Value?.ToString();
                        TipodeContratoTxtBox.Text = row.Cells["TipoContrato"].Value?.ToString();
                        Departamento.Text = row.Cells["Departamento"].Value?.ToString();
                        CargoTxtBox.Text = row.Cells["Cargo"].Value?.ToString();
                        CargaHorariaTxtBox.Text = row.Cells["HorasTrabalho"].Value?.ToString();
                        DtAdmissaoBox.Text = row.Cells["DataAdmissao"].Value?.ToString();
                        char beneficio = row.Cells["OutrosBeneficios"].Value?.ToString()[0] ?? '\0'; // Obtendo o char do DataGridView
                        ChkBeneficioI.Checked = beneficio == 'S';
                        BeneficioITxtBox.Text = row.Cells["NomeBeneficiosI"].Value?.ToString();
                        ValorITxtBox.Text = row.Cells["ValorBeneficioI"].Value?.ToString();
                        char vt = row.Cells["VT"].Value?.ToString()[0] ?? '\0'; // Obtendo o char do DataGridView
                        ChkSimVT.Checked = vt == 'S';
                        ChkNaoVT.Checked = vt == 'N';
                        ValorVTTxtBox.Text = row.Cells["ValorVT"].Value?.ToString();

                        EditarButton.Visible = true;
                        EditImg.Visible = true;
                        ExcluirButtton.Visible = true;
                        ExcluirImg.Visible = true;

        
            }
        }
        private void GerarFolhaPagamentoCompletaButton_Click(object sender, EventArgs e)
        {
            // Obtém a coleção de linhas selecionadas no DataGridView
            var linhasSelecionadas = DataGridEmpresas.SelectedRows;

            // Verifica se pelo menos uma linha está presente
            if (linhasSelecionadas.Count > 0)
            {
                // Caminho onde o PDF será salvo
                string caminhoPDF = @"D:\PDF Saved\Holerites.pdf";

                // Certifique-se de que o diretório existe
                string diretorio = Path.GetDirectoryName(caminhoPDF);
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                // Criação de um novo documento PDF
                using (var writer = new PdfWriter(caminhoPDF))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        // Itera sobre cada linha para gerar a folha de pagamento
                        foreach (DataGridViewRow linha in linhasSelecionadas)
                        {
                            // Chama o método para gerar a folha de pagamento passando o documento e a linha atual
                            GerarHoleritePDF(pdf, linha);
                        }
                    }
                }

                MessageBox.Show("Holerites gerados com sucesso!");
            }
            else
            {
                MessageBox.Show("Nenhuma linha selecionada. Selecione uma linha antes de gerar os holerites.");
            }
        }


        public static void GerarHoleritesPDF(List<DataGridViewRow> linhas)
        {
            // Caminho onde o PDF será salvo
            string caminhoPDF = @"D:\PDF Saved\Holerites.pdf";

            // Certifique-se de que o diretório existe
            string diretorio = Path.GetDirectoryName(caminhoPDF);
            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            // Criação de um novo documento PDF
            using (var writer = new PdfWriter(caminhoPDF))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Itera sobre cada linha para gerar a folha de pagamento
                    foreach (var linha in linhas)
                    {
                        // Chama o método para gerar o PDF passando o documento e a linha atual
                        GerarHoleritePDF(pdf, linha);

                        // Adiciona uma quebra de página entre cada folha de pagamento
                        document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                    }
                }
            }

            MessageBox.Show("Holerites gerados com sucesso!");
        }





        private void GerarFolhaPagamentoButton_Click(object sender, EventArgs e)
        {
            // Obtém a coleção de linhas selecionadas no DataGridView
            var linhasSelecionadas = DataGridEmpresas.SelectedRows;

            // Verifica se pelo menos uma linha está presente
            if (linhasSelecionadas.Count > 0)
            {
                // Cria uma nova lista para armazenar as linhas selecionadas
                var linhasSelecionadasList = new List<DataGridViewRow>();

                // Adiciona todas as linhas selecionadas à lista
                foreach (DataGridViewRow linha in linhasSelecionadas)
                {
                    linhasSelecionadasList.Add(linha);
                }

                // Chama o método para gerar o PDF passando a lista de linhas selecionadas
                GerarHoleritesPDF(linhasSelecionadasList);
            }
            else
            {
                MessageBox.Show("Nenhuma linha selecionada. Selecione uma linha antes de gerar o holerite.");
            }
        }



        private static void GerarHoleritePDF(PdfDocument PDf, DataGridViewRow row)
        {
            // Obtenha o diretório onde os PDFs são salvos
            string diretorioPDFs = @"D:\PDF Saved\";

            // Certifique-se de que o diretório existe
            if (!Directory.Exists(diretorioPDFs))
            {
                Directory.CreateDirectory(diretorioPDFs);
            }
            string nomeFuncionario = row.Cells["Nome"].Value?.ToString();
            string matricula = row.Cells["FuncionarioID"].Value?.ToString();


            // Cria o nome do arquivo inicial
            string nomeArquivo = $"Holerite - {nomeFuncionario}_{matricula}.pdf";        int contador = 1;
            // Verifica se o arquivo já existe 

            while (File.Exists(Path.Combine(diretorioPDFs, nomeArquivo)))
            {
                // Se existir, adiciona um número sequencial ao nome do arquivo
                nomeArquivo = $"Holerite - {nomeFuncionario}_{matricula}_{contador}.pdf";
                contador++;
            }

            // Caminho completo do arquivo PDF
            string caminhoPDF = Path.Combine(diretorioPDFs, nomeArquivo);
            // Calcular o salário líquido
            decimal salarioLiquido = CalcularSalarioLiquido(row);

            // Criação de um novo documento PDF
            using (var writer = new PdfWriter(caminhoPDF))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(PDf);

                    document.SetMargins(40, 40, 30, 30); // Defina margens 

                    // Criar um estilo para texto em negrito
                    Style estiloNegrito = new Style().SetBold();

                    // Adiciona os dados do DataGridView ao documento
                    document.Add(new Paragraph($"Demonstrativo de Pagamento - {DateTime.Now.ToString("MMMM yyyy")}")
                            .SetFontColor(DeviceRgb.BLACK)
                            .SetBold()
                            .SetFontSize(16f) );


                    document.Add(new Paragraph("").AddStyle(estiloNegrito)); //APENAS UM ESPACO


                    // Cria uma nova tabela com duas colunas
                    float larguraColun01 = 180f;
                    float larguraColun02 = 120f;              

                    Table tabelaEmpresaCnpj = new Table(UnitValue.CreatePointArray(new float[] { larguraColun01, larguraColun02 }));

                  //  Table tabelaEmpresaCnpj = new Table(2);
                    tabelaEmpresaCnpj.SetWidth(UnitValue.CreatePercentValue(70)); // Define a largura da tabela
                    // Cria células para o nome da empresa e do id
                    Cell cellEmpresa = new Cell();
                    Cell cellCnpj = new Cell();
                    document.Add(new Paragraph("Empresa:                                                           CNPJ:"));
                    // Adiciona os dados da empresa e do id às células 
                    cellEmpresa.Add(new Paragraph(row.Cells["NomeEmpresa"].Value?.ToString()));
                    cellCnpj.Add(new Paragraph(row.Cells["CNPJ"].Value?.ToString()));
                    // Adiciona as células à tabela
                    tabelaEmpresaCnpj.AddCell(cellEmpresa);
                    tabelaEmpresaCnpj.AddCell(cellCnpj);
                    // Adiciona a tabela ao documento
                    document.Add(tabelaEmpresaCnpj);



                    // Cria uma nova tabela com duas colunas

                    float larguraColun1 = 200f;
                    float larguraColun2 = 100f;

                    Table tabelaFuncionarioMatrícula = new Table(UnitValue.CreatePointArray(new float[] { larguraColun1, larguraColun2 }));
                    tabelaFuncionarioMatrícula.SetWidth(UnitValue.CreatePercentValue(70)); // Define a largura da tabela
                    // Cria células para o nome do funcionário e da matrícula
                    Cell cellNome = new Cell();
                    Cell cellMatrícula = new Cell();
                    document.Add(new Paragraph("Nome do funcionário:                                           Matrícula:"));
                    // Adiciona os dados do funcionário e da matrícula às células
                    cellNome.Add(new Paragraph(row.Cells["Nome"].Value?.ToString()));
                    cellMatrícula.Add(new Paragraph(row.Cells["FuncionarioID"].Value?.ToString()));
                    // Adiciona as células à tabela
                    tabelaFuncionarioMatrícula.AddCell(cellNome);
                    tabelaFuncionarioMatrícula.AddCell(cellMatrícula);
                    // Adiciona a tabela ao documento
                    document.Add(tabelaFuncionarioMatrícula);


                    // Cria uma nova tabela com duas colunas

                    float larguraColum1 = 170f;
                    float larguraColum2 = 130f;

                    Table tabelaCargoDep = new Table(UnitValue.CreatePointArray(new float[] { larguraColum1, larguraColum2 }));

                    tabelaCargoDep.SetWidth(UnitValue.CreatePercentValue(70)); // Define a largura da tabela
                    // Cria células para o nome da empresa e do id
                    Cell cellCargo = new Cell();
                    Cell cellDerp = new Cell();
                    document.Add(new Paragraph("Função:                                                    Departamento:"));
                    // Adiciona os dados da empresa e do id às células
                    cellCargo.Add(new Paragraph(row.Cells["Cargo"].Value?.ToString()));
                    cellDerp.Add(new Paragraph(row.Cells["Departamento"].Value?.ToString()));
                    // Adiciona as células à tabela
                    tabelaCargoDep.AddCell(cellCargo);
                    tabelaCargoDep.AddCell(cellDerp);
                    // Adi  ciona a tabela ao documento
                    document.Add(tabelaCargoDep);
                    document.Add(new Paragraph("Salário:").AddStyle(estiloNegrito));
                    document.Add(CriarTabela2("R$ ", row.Cells["SalarioBase"].Value?.ToString()));
                    // Adiciona as células à tabela

                    // Adiciona tabela para descontos adicionais
                    document.Add(new Paragraph("Informes:").AddStyle(estiloNegrito));

                    float larguraColuna1 = 200f;
                    float larguraColuna2 = 100f;
                    float larguraColuna3 = 100f;

                    Table tabela3 = new Table(UnitValue.CreatePointArray(new float[] { larguraColuna1, larguraColuna2, larguraColuna3 }));

                    decimal salarioBase = (decimal)row.Cells["SalarioBase"].Value;
                    decimal valorVT = (decimal)row.Cells["ValorVT"].Value;

                    // Cria células para os cabeçalhos e adiciona essas células à tabela
                    Cell cellDescricao = new Cell().Add(new Paragraph("Descrição").SetBold());
                    cellDescricao.SetTextAlignment(TextAlignment.CENTER);
                    cellDescricao.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    Cell cellQtde = new Cell().Add(new Paragraph("Qtde").SetBold());
                    cellQtde.SetTextAlignment(TextAlignment.CENTER);
                    cellQtde.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    Cell cellDescontos = new Cell().Add(new Paragraph("Descontos").SetBold());
                    cellDescontos.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontos.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    tabela3.AddCell(cellDescricao);
                    tabela3.AddCell(cellQtde);
                    tabela3.AddCell(cellDescontos);

                    // Cria células para os dados e adiciona essas células à tabela
                    
                    Cell cellValeTransporte = new Cell().Add(new Paragraph("Vale Transporte"));
                    Cell cellValorVT = new Cell().Add(new Paragraph(row.Cells["ValorVT"].Value?.ToString() + "%")); //
                    cellValorVT.SetTextAlignment(TextAlignment.CENTER);
                    cellValorVT.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    decimal descontoVT = CalcularDescontoVT(salarioBase, valorVT);
                    Cell cellDescontoVT = new Cell().Add(new Paragraph(descontoVT.ToString("C2")));  // Formatando como moeda
                    cellDescontoVT.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontoVT.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    Cell cellINSS = new Cell().Add(new Paragraph("INSS"));
                    Cell cellValorINSS = new Cell();
                    Cell cellDescontoINSS = new Cell();
                    cellValorINSS.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontoINSS.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    decimal descontoINSS = CalcularDescontoINSS(Convert.ToDecimal(row.Cells["SalarioBase"].Value));
                    
                    decimal valorInicial = salarioBase;
                    decimal valorFinal = salarioBase - descontoINSS;
                    
                    decimal diminuicaoPercentual = ((valorInicial - valorFinal) / valorInicial) * 100;
                    cellDescontoINSS.Add(new Paragraph(descontoINSS.ToString("C2")));  // Formatando como moeda
                    cellDescontoINSS.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontoINSS.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    diminuicaoPercentual = Math.Abs(diminuicaoPercentual);
                   // Formatação do valor como porcentagem
                    string porcentagemFormatada = diminuicaoPercentual.ToString("0.00") + "%";
                    cellValorINSS.Add(new Paragraph(porcentagemFormatada));  // Adiciona à célula formatando como porcentagem

                    decimal descontoIRRF = CalcularDescontoIRRF(salarioBase);
                    // Adiciona as informações às células correspondentes na tabela
                    Cell cellIRRF = new Cell().Add(new Paragraph("IRRF"));
                    valorInicial = salarioBase;
                    valorFinal = salarioBase - descontoIRRF;
                    diminuicaoPercentual = ((valorInicial - valorFinal) / valorInicial) * 100;
                    
                    porcentagemFormatada = diminuicaoPercentual.ToString("0.00") + "%";
                    Cell cellValorIRRF = new Cell();
                    cellValorIRRF.SetTextAlignment(TextAlignment.CENTER);
                    cellValorIRRF.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    cellValorIRRF.Add(new Paragraph(porcentagemFormatada));  // Adiciona à célula formatando como porcentagem
                    Cell cellDescontoIRRF = new Cell().Add(new Paragraph(descontoIRRF.ToString("C2")));  // Formatando como moeda
                    cellDescontoIRRF.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontoIRRF.SetVerticalAlignment(VerticalAlignment.MIDDLE);


                    string nomeBeneficioI = row.Cells["NomeBeneficiosI"].Value?.ToString();

                    Cell cellAdicional = new Cell().Add(new Paragraph(string.IsNullOrEmpty(nomeBeneficioI) ? "DESCONTO ADICIONAL" : nomeBeneficioI));
                    Cell cellValorAdicional = new Cell().Add(new Paragraph(string.IsNullOrEmpty(nomeBeneficioI) ? "0.00" : "1.00"));

                    cellValorAdicional.SetTextAlignment(TextAlignment.CENTER);
                    cellValorAdicional.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    decimal valorBeneficioI = Convert.ToDecimal(row.Cells["ValorBeneficioI"].Value);
                    string valorFormatado = valorBeneficioI.ToString("C2");
                    Cell cellDescontoAdicional = new Cell().Add(new Paragraph(valorFormatado));
                    cellDescontoAdicional.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontoAdicional.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    Cell cellHorasNormais = new Cell().Add(new Paragraph("Horas Normais"));
                    Cell cellQtdeHorasNormais = new Cell().Add(new Paragraph(row.Cells["HorasTrabalho"].Value?.ToString()+"hrs")); //
                    cellQtdeHorasNormais.SetTextAlignment(TextAlignment.CENTER);
                    cellQtdeHorasNormais.SetVerticalAlignment(VerticalAlignment.MIDDLE);
                    Cell cellDescontoHorasNormais = new Cell().Add(new Paragraph("0.00"));
                    cellDescontoHorasNormais.SetTextAlignment(TextAlignment.CENTER);
                    cellDescontoHorasNormais.SetVerticalAlignment(VerticalAlignment.MIDDLE);

                    tabela3.AddCell(cellValeTransporte);
                    tabela3.AddCell(cellValorVT);
                    tabela3.AddCell(cellDescontoVT);

                    tabela3.AddCell(cellINSS);
                    tabela3.AddCell(cellValorINSS);
                    tabela3.AddCell(cellDescontoINSS);

                    tabela3.AddCell(cellIRRF);
                    tabela3.AddCell(cellValorIRRF);
                    tabela3.AddCell(cellDescontoIRRF);

                    tabela3.AddCell(cellAdicional);
                    tabela3.AddCell(cellValorAdicional);
                    tabela3.AddCell(cellDescontoAdicional);

                    tabela3.AddCell(cellHorasNormais);
                    tabela3.AddCell(cellQtdeHorasNormais);
                    tabela3.AddCell(cellDescontoHorasNormais);


                    // Adiciona a tabela ao documento
                    document.Add(tabela3);



                    // Adicionar informações do salário líquido
                    Table tabelaSalarioLiquido = new Table(2);
                    document.Add(new Paragraph("").AddStyle(estiloNegrito));

                    // Adiciona célula para o texto "Salário Líquido"
                    Cell cellTextoSalarioLiquido = new Cell().Add(new Paragraph("Salário Líquido:"));
                    cellTextoSalarioLiquido.SetBackgroundColor(DeviceGray.GRAY); // Define a cor de fundo como cinza claro
                    // Adiciona célula para o valor do salário líquido
                    Cell cellValorSalarioLiquido = new Cell().Add(new Paragraph($"R$ {salarioLiquido.ToString("N2")}"));

                    // Adiciona as células à tabela
                    tabelaSalarioLiquido.AddCell(cellTextoSalarioLiquido);
                    tabelaSalarioLiquido.AddCell(cellValorSalarioLiquido);

                    // Adiciona a tabela ao documento
                    document.Add(tabelaSalarioLiquido);

                }
            }

            MessageBox.Show("Holerite gerado com sucesso!");
        }



        private static Table CriarTabela(string rotulo, string valor)
        {
            Table tabela = new Table(2);
            tabela.SetWidth(UnitValue.CreatePercentValue(60)); // Defina a largura da tabela

            Cell cellRotulo = new Cell().Add(new Paragraph(rotulo));
            Cell cellValor = new Cell().Add(new Paragraph(valor));

            // Adicione bordas às células
            cellRotulo.SetBorder(Border.NO_BORDER);
            cellValor.SetBorder(Border.NO_BORDER);

            tabela.AddCell(cellRotulo);
            tabela.AddCell(cellValor);

            // Adicione bordas à tabela
             tabela.SetBorder(new SolidBorder(DeviceRgb.BLACK, 1)); // Espessura da borda: 1 ponto

            return tabela;
        }

        private static Table CriarTabela2(string rotulo, string valor)
        {
            Table tabela2 = new Table(2);
            tabela2.SetWidth(UnitValue.CreatePercentValue(20)); // Defina a largura da tabela
            Cell cellRotulo = new Cell().Add(new Paragraph(rotulo));
            Cell cellValor = new Cell().Add(new Paragraph(valor));
            // Adicione bordas às células
            cellRotulo.SetBorder(Border.NO_BORDER);
            cellValor.SetBorder(Border.NO_BORDER);
            tabela2.AddCell(cellRotulo);
            tabela2.AddCell(cellValor);
            // Adicione bordas à tabela
            tabela2.SetBorder(new SolidBorder(DeviceRgb.BLACK, 1)); // Espessura da borda: 1 ponto
            return tabela2;
        }


        private static decimal CalcularSalarioLiquido(DataGridViewRow row)
        {
            decimal salarioBase = (decimal)row.Cells["SalarioBase"].Value;
            decimal descontoINSS = CalcularDescontoINSS(salarioBase);
            decimal descontoIRRF = CalcularDescontoIRRF(salarioBase);
            decimal valorVT = (decimal)row.Cells["ValorVT"].Value;
            decimal valorBeneficioI = (decimal)row.Cells["ValorBeneficioI"].Value;

            decimal salarioLiquido = salarioBase - descontoINSS - descontoIRRF - valorBeneficioI - valorVT;

            return salarioLiquido;
        }

        private static decimal CalcularDescontoINSS(decimal salarioBase)
        {
            decimal descontoINSS = 0;

            // Faixas salariais e suas respectivas alíquotas
            decimal faixa1 = 1320.00m;
            decimal faixa2 = 2571.29m;
            decimal faixa3 = 3856.94m;
            decimal faixa4 = 7507.29m;

            // Alíquotas correspondentes às faixas
            decimal aliquotaFaixa1 = 0.075m;  // 7.5%
            decimal aliquotaFaixa2 = 0.09m;   // 9%
            decimal aliquotaFaixa3 = 0.12m;   // 12%
            decimal aliquotaFaixa4 = 0.14m;   // 14%

            // Calcular o desconto para cada faixa
            decimal descontoFaixa1 = Math.Min(faixa1, salarioBase) * aliquotaFaixa1;
            decimal descontoFaixa2 = Math.Max(0, Math.Min(faixa2 - faixa1, salarioBase - faixa1)) * aliquotaFaixa2;
            decimal descontoFaixa3 = Math.Max(0, Math.Min(faixa3 - faixa2, salarioBase - faixa2)) * aliquotaFaixa3;
            decimal descontoFaixa4 = Math.Max(0, Math.Min(faixa4 - faixa3, salarioBase - faixa3)) * aliquotaFaixa4;

            // Somar os descontos das faixas
            descontoINSS = descontoFaixa1 + descontoFaixa2 + descontoFaixa3 + descontoFaixa4;

            return descontoINSS;
        }

        private static decimal CalcularDescontoVT(decimal salarioBase, decimal valorVT)
        {
            decimal descontoVT = salarioBase * (valorVT / 100);
            return descontoVT;
        }

        private static decimal CalcularDescontoIRRF(decimal salarioBase)
        {
            // Faixas salariais e suas respectivas alíquotas
            decimal faixa1 = 1903.98m;
            decimal faixa2 = 2826.65m;
            decimal faixa3 = 3751.05m;
            decimal faixa4 = 4664.68m;

            // Alíquotas correspondentes às faixas
            decimal aliquotaFaixa1 = 0.075m;  // 7.5%
            decimal aliquotaFaixa2 = 0.15m;   // 15%
            decimal aliquotaFaixa3 = 0.225m;  // 22.5%
            decimal aliquotaFaixa4 = 0.275m;  // 27.5%

            // Calcular o desconto do IRRF para cada faixa
            decimal irrf;

            if (salarioBase <= faixa1)
            {
                irrf = 0;
            }
            else if (salarioBase <= faixa2)
            {
                irrf = salarioBase * aliquotaFaixa1 - 142.80m;
            }
            else if (salarioBase <= faixa3)
            {
                irrf = salarioBase * aliquotaFaixa2 - 354.80m;
            }
            else if (salarioBase <= faixa4)
            {
                irrf = salarioBase * aliquotaFaixa3 - 636.13m;
            }
            else
            {
                irrf = salarioBase * aliquotaFaixa4 - 869.36m;
            }

            // Garantir que o IRRF não seja negativo
            return Math.Max(irrf, 0);
        }


        //BOTAO DE EDITAR:
        private void Editar_Click(object sender, EventArgs e)
        {
            EDITARbtn.Visible = true;
            CancelarButton.Visible = true;
            ExcluirButtton.Visible = false;
            ExcluirImg.Visible = false;
        }

        //CLICAR FORA VAI CANCELAR A ATIVIDADE TAMBEM:
        private void Funcionarios_MouseClick(object sender, MouseEventArgs e)
        {
            LimparCampos();
            
            EditarButton.Visible = false;
            ExcluirButtton.Visible = false;
            EDITARbtn.Visible = false;
            EditImg.Visible = false;
            ExcluirButtton.Visible = false;
            ExcluirImg.Visible = false;
        }

        //BOTAO DE NOVO CADASTRO:
        private void NovoCadastro_Click_1(object sender, EventArgs e)
        {
            LimparCampos();
            SalvarButton.Visible = true;
            CancelarButton.Visible = true;
            EditarButton.Visible = false;
            EditImg.Visible = false;
            ExcluirButtton.Visible = false;
            ExcluirImg.Visible = false;
        }

        //ENVIAR A EDIÇÃO NO BANCO DE DADOS (UPDATE):
        private void EDITARbtn_Click(object sender, EventArgs e)
        {
            if (comboBoxEmpresas.SelectedItem != null)
            {
                int empresaID = (int)comboBoxEmpresas.SelectedValue;

                if (chkMasculino.Checked)
                {
                    sexo = 'M';
                }
                else if (chkFeminino.Checked)
                {
                    sexo = 'F';
                }
                else if (chkOutros.Checked)
                {
                    sexo = 'O';

                }
                if (ChkBeneficioI.Checked)
                {
                    Beneficio = 'S';
                }
                if (ChkSimVT.Checked)
                {
                    VT = 'S';
                }
                if (ChkNaoVT.Checked)
                {
                    VT = 'N';
                }
                if (decimal.TryParse(SalarioBTxtBox.Text, out salario) &&
                int.TryParse(CargaHorariaTxtBox.Text, out cargaHoraria) &&
                decimal.TryParse(ValorITxtBox.Text, out valorI) &&
                 decimal.TryParse(ValorVTTxtBox.Text, out valorVT))
                {

                }
                var pessoa = new Pessoa(Convert.ToInt32(IDtext.Text), Convert.ToInt32(empresaID), NomeTxtBox.Text, CPFMakedtBox.Text, NascimentoBox.Text, sexo, SexualidadOutrosTxtBox.Text, RuaTxtBox.Text, NTxtBox.Text,
                          CEPMakedtBox.Text, UFTxtBox.Text, CidadeTxtBox.Text, TelefoneAMakedtBox.Text, EMailTxtBox.Text, salario, TipodeContratoTxtBox.Text, Departamento.Text, CargoTxtBox.Text,
                          cargaHoraria, DtAdmissaoBox.Text, Beneficio, BeneficioITxtBox.Text, valorI, VT, valorVT);

                var pessoaRepositorio = new PessoaRepositorio();
                pessoaRepositorio.AtualizarFuncionario(pessoa);
                LimparCampos();
                MessageBox.Show("Salvo com Sucesso");
                CarregarDadosNoDataGridView();

                EditarButton.Visible = false;
                EditImg.Visible = false;

                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;
                NovoCadastro.Visible = true;
                EDITARbtn.Visible = false;
                CancelarButton.Visible = false;
            }

        }

        //BOTAO DE EXCLUIR :
        private void ExcluirButtton_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("Tem certeza que deseja *Excluir* o cadastro selecionado?", "Cancelar Cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                var pessoaRepositorio = new PessoaRepositorio();
                pessoaRepositorio.DeletarFuncionario(Convert.ToInt32(IDtext.Text));
                LimparCampos();
                MessageBox.Show("Cadastro excluído com Sucesso!");
                CarregarDadosNoDataGridView();
                NovoCadastro.Visible = true;
                CancelarButton.Visible = false;
                EditarButton.Visible = false;
                EditImg.Visible = false;
                SalvarButton.Visible = false;
                EDITARbtn.Visible = false;
                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;
            }
            else if (resultado == DialogResult.No)
            {
                LimparCampos();
                NovoCadastro.Visible = true;
                CancelarButton.Visible = false;
                EditarButton.Visible = false;
                EditImg.Visible = false;
                SalvarButton.Visible = false;
                EDITARbtn.Visible = false;
                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;
            }
        }

        //BOTAO DE EXCLUSAO DO CADASTRO NO BD (DELETE):
        private void ExcluirImg_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Tem certeza que deseja *Excluir* o cadastro selecionado?", "Cancelar Cadastro", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                var pessoaRepositorio = new PessoaRepositorio();
                pessoaRepositorio.DeletarFuncionario(Convert.ToInt32(IDtext.Text));
                LimparCampos();
                MessageBox.Show("Cadastro excluído com Sucesso!");
                CarregarDadosNoDataGridView();
                NovoCadastro.Visible = true;
                CancelarButton.Visible = false;
                EditarButton.Visible = false;
                EditImg.Visible = false;
                SalvarButton.Visible = false;
                EDITARbtn.Visible = false;
                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;

            }
            else if (resultado == DialogResult.No)
            {
                LimparCampos();

                NovoCadastro.Visible = true;
                CancelarButton.Visible = false;
                EditarButton.Visible = false;
                EditImg.Visible = false;
                SalvarButton.Visible = false;
                EDITARbtn.Visible = false;
                ExcluirButtton.Visible = false;
                ExcluirImg.Visible = false;
            }
        }


        //BOTAO DE EDIÇÃO
        private void EditImg_Click(object sender, EventArgs e)
        {
            ExcluirButtton.Visible = false;
            ExcluirImg.Visible = false;
            EDITARbtn.Visible = true;
            CancelarButton.Visible = true;
        }

        //LIMPAR BARRA DE PESQUISA
        private void PesquisaBar_MouseClick(object sender, MouseEventArgs e)
        {
            PesquisaBar.Text = "";

        }




 










    }
}
