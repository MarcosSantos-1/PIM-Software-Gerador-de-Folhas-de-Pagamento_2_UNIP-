using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.OData.Edm;


namespace HorizonHR.Repositorio
{
    public class DbConexao : IDisposable
    {
        private readonly IDbConnection connection;        
        public DbConexao()
        {
             connection = new SqlConnection(@"Integrated Security =SSPI; Persist Security Info=False; Initial Catalog= HorizonDB ;Data Source=(localdb)\MSSQLLocalDB");
        }
   public IDbConnection GetConnection() 
        { 
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }
    public void Dispose()
        {
            connection?.Dispose();
        }
    }
}







