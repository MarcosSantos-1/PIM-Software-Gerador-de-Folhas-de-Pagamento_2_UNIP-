using iText.Kernel.Pdf;
using iText.Layout.Element;
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
using iText.Layout;


namespace HorizonHR.Repositorio
{
    public partial class FolhaPagamento_TEste : Form
    {/*
        public FolhaPagamento_TEste()
        {
            InitializeComponent();
        }
        private void btnGerarHolerite_Click(object sender, EventArgs e)
        {
            int idFuncionario = Convert.ToInt32(txtIdFuncionario.Text);

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                connection.Open();
                string query = $"SELECT * FROM Funcionarios WHERE IdFuncionario = {idFuncionario}";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string nome = reader["Nome"].ToString();
                    string empresa = reader["Empresa"].ToString();
                    string cargo = reader["Cargo"].ToString();
                    string departamento = reader["Departamento"].ToString();
                    DateTime dataAdmissao = Convert.ToDateTime(reader["DataAdmissao"]);
                    decimal salarioBase = Convert.ToDecimal(reader["SalarioBase"]);
                    int horasTrabalhadas = Convert.ToInt32(reader["HorasTrabalhadas"]);

                    decimal inss = CalcularINSS(salarioBase);
                    decimal irff = CalcularIRFF(salarioBase);
                    decimal salarioMinimo = salarioBase / SalarioMinimo();

                    // Criar PDF
                    PdfWriter writer = new PdfWriter("Holerite.pdf");
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf);
                    document.Add(new Paragraph($"ID Funcionário: {idFuncionario}"));
                    document.Add(new Paragraph($"Nome: {nome}"));
                    document.Add(new Paragraph($"Empresa: {empresa}"));
                    document.Add(new Paragraph($"Cargo: {cargo}"));
                    document.Add(new Paragraph($"Departamento: {departamento}"));
                    document.Add(new Paragraph($"Data de Admissão: {dataAdmissao.ToShortDateString()}"));
                    document.Add(new Paragraph($"Salário Base: {salarioBase}"));
                    document.Add(new Paragraph($"Horas Trabalhadas: {horasTrabalhadas}"));
                    document.Add(new Paragraph($"INSS: {inss}"));
                    document.Add(new Paragraph($"IRFF: {irff}"));
                    document.Add(new Paragraph($"Total em Salários Mínimos: {salarioMinimo}"));

                    document.Close();

                    MessageBox.Show("Holerite gerado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Funcionário não encontrado!");
                }
            }
        }

        private decimal CalcularINSS(decimal salarioBase)
        {
            // Lógica para calcular o INSS
            // Implemente conforme as regras do seu país/região
            // Exemplo: 10% do salário base
            return salarioBase * 0.1m;
        }

        private decimal CalcularIRFF(decimal salarioBase)
        {
            // Lógica para calcular o IRFF
            // Implemente conforme as regras do seu país/região
            // Exemplo: 15% do salário base
            return salarioBase * 0.15m;
        }

        private decimal SalarioMinimo()
        {
            // Valor do salário mínimo no seu país/região
            // Exemplo: R$ 1000,00
            return 1000.00m;
        }*/
    }
}