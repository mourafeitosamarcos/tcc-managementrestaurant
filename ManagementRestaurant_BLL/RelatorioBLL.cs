using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class RelatorioBLL
    {
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private readonly RelatorioDAL _RelatorioDAL = new RelatorioDAL();

        public ConexaoMDL quantidadePedidoTp(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_quantidade_produto_tipo(relatorioMDL);
        }

        public ConexaoMDL quantidadePedidoEntrada(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_quantidade_produto_Entrada(relatorioMDL);
        }

        public ConexaoMDL quantidadeVeiculoCad(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_quantidade_veiculos_cad(relatorioMDL);
        }

        public ConexaoMDL quantidadeClienteEstado(RelatorioMDL relatorioMDL) 
        {
            return _RelatorioDAL.spc_bi_quantidade_Cliente_Estado(relatorioMDL);
        }        

        public ConexaoMDL totalDocumentoElemento(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_total_doc_elemento(relatorioMDL);
        }

        public ConexaoMDL funcionarioGeral(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_funcionario_geral(relatorioMDL);
        }

        public ConexaoMDL agendamentosDia(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_quantidade_agendamentos_dia(relatorioMDL);
        }

        public ConexaoMDL quantidadePedidoTpCliente(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_quantidade_ped_tipo_cli(relatorioMDL);
        }

        public ConexaoMDL funcionarioTunOver(RelatorioMDL relatorioMDL)
        {
            return _RelatorioDAL.spc_bi_funcionario_turnover(relatorioMDL);
        }
    }
}