using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HorizonHR
{

    //--------------FUNCIONARIOS--------------------
    public class Pessoa
    {
       
        public Pessoa() { }
        //Contrutor   23
        public Pessoa(int funcionarioId, int empresaId, string nome, string cPF, string dataNascimento, char sexo, string outroSexo, string rua, string nResidencia, string cEP, string uF, string cidade, string telefoneA, string eMail, decimal salarioBase, string tipoContrato, string departamento, string cargo,  int horasTrabalho, string dataAdmissao, char outrosBeneficios, string nomeBeneficiosI, decimal valorBeneficioI, char vT, decimal valorVT)

        {
            FuncionarioId = funcionarioId;
            EmpresaId = empresaId;
            Nome = nome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            OutroSexo = outroSexo;
            Rua = rua;
            this.nResidencia = nResidencia;
            CEP = cEP;
            UF = uF;
            Cidade = cidade;
            TelefoneA = telefoneA;
            this.eMail = eMail;
            SalarioBase = salarioBase;
            TipoContrato = tipoContrato;
            Departamento = departamento;
            Cargo = cargo;
            HorasTrabalho = horasTrabalho;
            DataAdmissao = dataAdmissao;
            OutrosBeneficios = outrosBeneficios;
            NomeBeneficiosI = nomeBeneficiosI;
            ValorBeneficioI = valorBeneficioI;
            VT = vT;
            ValorVT = valorVT;
        }

        //Propriedades da classe Pessoa
        public int FuncionarioId { get;  set; }
        public int EmpresaId { get; set; }
        public string Nome { get;  set; }
        public string CPF { get;  set; }
        public string DataNascimento { get;  set; }
        public char Sexo { get;  set; }
        public string OutroSexo { get;  set; }
        public string Rua { get;  set; }
        public string nResidencia { get;  set; }
        public string CEP { get;  set; }
        public string UF { get;  set; }
        public string Cidade { get;  set; }
        public string TelefoneA { get;  set; }
        public string eMail { get;  set; }
        public decimal SalarioBase { get;  set; }
        public string TipoContrato { get;  set; }
        public string Departamento { get; set; }
        public string Cargo { get;  set; }
        public int HorasTrabalho { get;  set; }
        public string DataAdmissao { get;  set; }
        public char OutrosBeneficios { get;  set; }
        public string NomeBeneficiosI { get;  set; }
        public decimal ValorBeneficioI { get; set; }
        public char VT { get; set; }
        public decimal ValorVT { get;  set; }
      
    }

    //--------------EMPRESAS--------------------
  

        public class Empresa
    {
         Empresa() { }   
        public Empresa(int empresaId, string nome, string segmento, string cNPJ, string rua, string nResidencia, string cEP, string uF, 
            string cidade, string bairro, string complemento, 
            string telefoneA, string telefoneB, string eMailI, string eMailII)
        {

            EmpresaID = empresaId;
            Nome = nome;
            Segmento = segmento;
            CNPJ = cNPJ;

            Rua = rua;
            this.nResidencia = nResidencia;
            CEP = cEP;
            UF = uF;
            Cidade = cidade;
            Bairro = bairro;
            Complemento = complemento;
            TelefoneA = telefoneA;
            TelefoneB = telefoneB;
            this.eMailI = eMailI;
            this.eMailII = eMailII;
        }

        public int EmpresaID { get; private set; }
        public string Nome { get; private set; }
        public string Segmento { get; private set; }
        public string CNPJ { get; private set; }
        public string Rua { get; private set; }
        public string nResidencia { get; private set; }
        public string CEP { get; private set; }
        public string UF { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Complemento { get; private set; }
        public string TelefoneA { get; private set; }
        public string TelefoneB { get; private set; }
        public string eMailI { get; private set; }
        public string eMailII { get; private set; }



    }



    public class DadosEmpresa
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public string Segmento { get; set; }

        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Adicione outras propriedades conforme necessário para as outras TextBoxes
    }
}
