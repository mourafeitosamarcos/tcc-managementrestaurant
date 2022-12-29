using System.Configuration;
using System.Data.SqlClient;

namespace ManagementRestaurant_DAL
{
    public class ConexaoDAL
    {
        #region ConnectionString

        public readonly SqlConnection Conexao = new SqlConnection
            (ConfigurationManager.ConnectionStrings["cs_ManagementRestaurant"].ToString());

        #endregion
    }
}