using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HorizonHR.Repositorio;
using Dapper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace HorizonHR
{
    public class PessoaRepositorio
    {

        //INSERIR FORMS CADASTRO FUNCIONARIO
        public void Inserir(Pessoa pessoa)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            var funcionarioId = connection.ExecuteScalar<int>(@"INSERT INTO FUNCIONARIOS (EmpresaID, Nome ,CPF, DataNascimento, Sexo, OutroSexo, Rua, nResidencia, CEP, UF, Cidade, TelefoneA, eMail, 
SalarioBase, TipoContrato , Departamento, Cargo,  HorasTrabalho, DataAdmissao, OutrosBeneficios, NomeBeneficiosI, ValorBeneficioI, VT, ValorVT) VALUES (@EmpresaID, @Nome ,@CPF, @DataNascimento, @Sexo, @OutroSexo, @Rua, @nResidencia, @CEP, @UF, @Cidade, @TelefoneA, @eMail, 
@SalarioBase, @TipoContrato , @Departamento, @Cargo,  @HorasTrabalho, @DataAdmissao, @OutrosBeneficios, @NomeBeneficiosI, @ValorBeneficioI, @VT, @ValorVT); SELECT SCOPE_IDENTITY();",
            new
            {
                pessoa.EmpresaId,
                pessoa.Nome,
                pessoa.CPF,
                pessoa.DataNascimento,                
                pessoa.Sexo,
                pessoa.OutroSexo,            
                pessoa.Rua,
                pessoa.nResidencia,
                pessoa.CEP,
                pessoa.UF,
                pessoa.Cidade,
                pessoa.TelefoneA,      
                pessoa.eMail,
                pessoa.SalarioBase,
                pessoa.TipoContrato,                
                pessoa.Departamento,
                pessoa.Cargo,
                pessoa.HorasTrabalho,
                pessoa.DataAdmissao,
                pessoa.OutrosBeneficios,
                pessoa.NomeBeneficiosI,
                pessoa.ValorBeneficioI,
                pessoa.VT,
                pessoa.ValorVT,

            }); ;
        }

        public void AtualizarFuncionario(Pessoa pessoa)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();

            // Atualizar dados na tabela FUNCIONARIOS
            connection.Execute(
                @"UPDATE FUNCIONARIOS 
        SET EmpresaID = @EmpresaID, 
            Nome = @Nome, 
            CPF = @CPF, 
            DataNascimento = @DataNascimento, 
            Sexo = @Sexo, 
            OutroSexo = @OutroSexo, 
            Rua = @Rua, 
            nResidencia = @nResidencia, 
            CEP = @CEP, 
            UF = @UF, 
            Cidade = @Cidade, 
            TelefoneA = @TelefoneA, 
            eMail = @eMail, 
            SalarioBase = @SalarioBase, 
            TipoContrato = @TipoContrato, 
            Departamento = @Departamento, 
            Cargo = @Cargo, 
            HorasTrabalho = @HorasTrabalho, 
            DataAdmissao = @DataAdmissao, 
            OutrosBeneficios = @OutrosBeneficios, 
            NomeBeneficiosI = @NomeBeneficiosI, 
            ValorBeneficioI = @ValorBeneficioI, 
            VT = @VT, 
            ValorVT = @ValorVT 
        WHERE FuncionarioID = @FuncionarioID;",
                new
                {
                    pessoa.FuncionarioId,  // Suponho que você tenha a propriedade FuncionarioID na classe Pessoa
                    pessoa.EmpresaId,
                    pessoa.Nome,
                    pessoa.CPF,
                    pessoa.DataNascimento,
                    pessoa.Sexo,
                    pessoa.OutroSexo,
                    pessoa.Rua,
                    pessoa.nResidencia,
                    pessoa.CEP,
                    pessoa.UF,
                    pessoa.Cidade,
                    pessoa.TelefoneA,
                    pessoa.eMail,
                    pessoa.SalarioBase,
                    pessoa.TipoContrato,
                    pessoa.Departamento,
                    pessoa.Cargo,
                    pessoa.HorasTrabalho,
                    pessoa.DataAdmissao,
                    pessoa.OutrosBeneficios,
                    pessoa.NomeBeneficiosI,
                    pessoa.ValorBeneficioI,
                    pessoa.VT,
                    pessoa.ValorVT
                });
        }

        public void DeletarFuncionario(int funcionarioId)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();

            // Atualizar dados na tabela FUNCIONARIOS
            connection.Execute(
                @"DELETE FROM FUNCIONARIOS 
        WHERE FuncionarioID = @FuncionarioID;",
                new
                {
                    funcionarioId 
                    
                });
        }
        //INSERIR FORMS CADASTRO - EMPRESA
        public void InserirEmpresa(Empresa empresa)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            // Inserir dados na tabela EMPRESA

            var empresaId = connection.ExecuteScalar<int>(
                @"INSERT INTO EMPRESAS (Nome, Segmento, CNPJ) VALUES (@Nome, @Segmento, @CNPJ);
            SELECT SCOPE_IDENTITY();",
                new { empresa.Nome, empresa.Segmento, empresa.CNPJ });

            // Inserir dados na tabela ENDERECO_EMPRESA usando o ID da empresa inserida anteriormente
            connection.Execute(
                @"INSERT INTO ENDERECO_EMPRESA (EmpresaID, Rua, nResidencia, CEP, UF, Cidade, Bairro, Complemento, TelefoneA, TelefoneB, eMailI, EmailII)
            VALUES (@EmpresaID, @Rua, @nResidencia, @CEP, @UF, @Cidade, @Bairro, @Complemento, @TelefoneA, @TelefoneB, @eMailI, @EmailII);",
                new
                {
                    EmpresaID = empresaId,
                    empresa.Rua,
                    empresa.nResidencia,
                    empresa.CEP,
                    empresa.UF,
                    empresa.Cidade,
                    empresa.Bairro,
                    empresa.Complemento,
                    empresa.TelefoneA,
                    empresa.TelefoneB,
                    empresa.eMailI,
                    empresa.eMailII
                });
        }


        public void AtualizarEmpresa(Empresa empresa)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();

            // Atualizar dados na tabela EMPRESAS
            connection.Execute(
                @"UPDATE EMPRESAS SET Nome = @Nome, Segmento = @Segmento, CNPJ = @CNPJ WHERE EmpresaID = @EmpresaID;",
                new { empresa.Nome, empresa.Segmento, empresa.CNPJ, empresa.EmpresaID });

            // Atualizar dados na tabela ENDERECO_EMPRESA usando o ID da empresa
            connection.Execute(
                @"UPDATE ENDERECO_EMPRESA 
        SET Rua = @Rua, nResidencia = @nResidencia, CEP = @CEP, UF = @UF, Cidade = @Cidade, Bairro = @Bairro, 
        Complemento = @Complemento, TelefoneA = @TelefoneA, TelefoneB = @TelefoneB, eMailI = @eMailI, EmailII = @EmailII 
        WHERE EmpresaID = @EmpresaID;",
                new
                {
                    empresa.EmpresaID,
                    empresa.Rua,
                    empresa.nResidencia,
                    empresa.CEP,
                    empresa.UF,
                    empresa.Cidade,
                    empresa.Bairro,
                    empresa.Complemento,
                    empresa.TelefoneA,
                    empresa.TelefoneB,
                    empresa.eMailI,
                    empresa.eMailII
                });
        }

        public List<Empresa> ObterEmpresasDoBancoDeDados()
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            {            
                return connection.Query<Empresa>("SELECT EMPRESAS.EmpresaID AS #,EMPRESAS.Nome, ENDERECO_EMPRESA.Cidade , ENDERECO_EMPRESA.UF, ENDERECO_EMPRESA.eMailI FROM EMPRESAS JOIN ENDERECO_EMPRESA ON EMPRESAS.EmpresaID = ENDERECO_EMPRESA.EmpresaID").ToList();
            }
        }

        public void DeletarEmpresa(int id)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            connection.Execute("DELETE FROM EMPRESAS  WHERE WHERE EmpresaID = @EmpresaID;", 
            new
                {
                    id 
                    
                }); ;
        }
        public Pessoa BuscarPessoaPeloID(int id)
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            return connection.QueryFirstOrDefault<Pessoa>(@"SELECT * FROM FUNCIONARIOS WHERE Id = @Id",
                new {id}); ;

        }
        public IEnumerable<Pessoa> BuscarTodasPessoas()
        {
            SqlConnection connection = (SqlConnection)new DbConexao().GetConnection();
            return connection.Query<Pessoa>(@"SELECT * FROM FUNCIONARIOS ");

        }

    }
}
