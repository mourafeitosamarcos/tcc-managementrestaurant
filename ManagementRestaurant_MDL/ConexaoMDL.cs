using System;
using System.Data;
using System.Data.SqlClient;

namespace ManagementRestaurant_MDL
{
    public class ConexaoMDL
    {
        public SqlCommand Cmd = new SqlCommand();
        public SqlDataAdapter Da = new SqlDataAdapter();
        public DataSet Ds = new DataSet();
        public DataSet Ds2 = new DataSet();
        public DataTable Dt = new DataTable();

        public Boolean Validador { get; set; }
        public String NRelatorio { get; set; }
        public String CRelatorio { get; set; }
        public Boolean Validador2 { get; set; }
    }
}