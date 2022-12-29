using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_DAL
{
    public class RelatorioDAL
    {
        private readonly ConexaoDAL _conexaoDAL = new ConexaoDAL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        public ConexaoMDL spc_bi_quantidade_produto_tipo(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_quantidade_produto_tipo") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipoProd", relatorioMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_quantidade_produto_Entrada(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_quantidade_produto_entrada") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@dtInicio", relatorioMDL.dtInicio));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@dtFim", relatorioMDL.dtFim));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codProd", relatorioMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();


            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_quantidade_veiculos_cad(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_quantidade_veiculos_cadastrados") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@dtInicio", relatorioMDL.dtInicio));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@dtFim", relatorioMDL.dtFim));            

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_quantidade_Cliente_Estado(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_quantidade_cliente_estado") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@estado", relatorioMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_total_doc_elemento(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_total_doc_elemento") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codDepto", relatorioMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_funcionario_geral(RelatorioMDL relatorioMDL)
        {

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_funcionario_geral") { CommandType = CommandType.StoredProcedure };
            //_conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@codDepto", relatorioMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_quantidade_agendamentos_dia(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_quantidade_agendamentos_dia") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@dtInicio", relatorioMDL.dtInicio));
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@dtFim", relatorioMDL.dtFim));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_quantidade_ped_tipo_cli(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_quantidade_ped_tipo_cli") { CommandType = CommandType.StoredProcedure };
            _conexaoMDL.Cmd.Parameters.Add(new SqlParameter("@tipoCli", relatorioMDL.Tipo));

            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

        public ConexaoMDL spc_bi_funcionario_turnover(RelatorioMDL relatorioMDL)
        {
            _conexaoDAL.Conexao.Open();

            _conexaoMDL.Cmd = new SqlCommand("spc_bi_funcionario_turnover") { CommandType = CommandType.StoredProcedure };
            
            _conexaoMDL.Cmd.Connection = _conexaoDAL.Conexao;
            _conexaoMDL.Da = new SqlDataAdapter(_conexaoMDL.Cmd);
            _conexaoMDL.Da.Fill(_conexaoMDL.Dt);

            _conexaoDAL.Conexao.Close();

            return _conexaoMDL;
        }

    }
}