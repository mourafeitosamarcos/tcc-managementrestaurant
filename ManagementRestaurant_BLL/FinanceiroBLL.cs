using System;
using ManagementRestaurant_DAL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class FinanceiroBLL
    {
        private readonly FinanceiroDAL _financeiroDAL = new FinanceiroDAL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CarregaDadosDepartamento
        public FinanceiroMDL CarregaDadosDepartamento(FinanceiroMDL _financeiroMDL)
        {
        
            
            _conexaoMDL = _financeiroDAL.spc_carrega_dados_departamento(_financeiroMDL);

            _financeiroMDL.CDepartamento = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Dep"].ToString());
            _financeiroMDL.NDepartamento = _conexaoMDL.Ds.Tables[0].Rows[0]["Nome_Dep"].ToString();
            if (string.IsNullOrEmpty(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_CC"].ToString()))
            {
                _financeiroMDL.CContrato = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Contrato"].ToString());
            }
            else
            {
                _financeiroMDL.CCusto = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_CC"].ToString());
            }
            _financeiroMDL.Responsavel = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();
          
            return _financeiroMDL;
        }
        #endregion

        #region AtualizaDepartamento

        public ConexaoMDL AtualizaDepartamento(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.AtualizaDepartamento(financeiroMDL);
        }

        #endregion

        #region PesquisaDepartamento
        public ConexaoMDL PesquisaDepartamento()
        {
            return _financeiroDAL.spc_pesquisa_departamento();
        }
        #endregion
    
        #region PesquisaCentroCusto
        public ConexaoMDL PesquisaCentroCusto()
        {
            return _financeiroDAL.spc_pesquisa_centro_custo();
        }
        #endregion

        #region PesquisaContrato
        public ConexaoMDL PesquisaContrato()
        {
            return _financeiroDAL.spc_pesquisa_contrato();
        }
        #endregion

        #region CadastraCentroCusto

        public ConexaoMDL CadastraCentroCusto(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.CadastraCentroCusto(financeiroMDL);
        }

        #endregion

        #region AtualizaCentroCusto

        public ConexaoMDL AtualizaCentroCusto(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.AtualizaCentroCusto(financeiroMDL);
        }

        #endregion

        #region AtualizaContrato

        public ConexaoMDL AtualizaContrato(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.AtualizaContrato(financeiroMDL);
        }

        #endregion

        #region CadastraContrato

        public ConexaoMDL CadastraContrato(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.CadastraContrato(financeiroMDL);
        }

        #endregion

        #region CadastraDepartamento

        public ConexaoMDL CadastraDepartamento(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.CadastraDepartamento(financeiroMDL);
        }

        #endregion

        #region CarregaDadosContrato
        public FinanceiroMDL CarregaDadosContrato(FinanceiroMDL _financeiroMDL)
        {
            _conexaoMDL = _financeiroDAL.spc_carrega_dados_contrato(_financeiroMDL);
            _financeiroMDL.CContrato = Convert.ToInt64(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Contrato"].ToString());
            _financeiroMDL.NContrato = _conexaoMDL.Ds.Tables[0].Rows[0]["Nome_Contrato"].ToString();
            _financeiroMDL.CConsolidado = _conexaoMDL.Ds.Tables[0].Rows[0]["Cliente_Consolidado"].ToString();
            return _financeiroMDL;
        }
        #endregion

        #region CarregaDadosCentroCusto
        public FinanceiroMDL CarregaDadosCentroCusto(FinanceiroMDL _financeiroMDL)
        {
        _conexaoMDL = _financeiroDAL.spc_carrega_dados_centro_custo(_financeiroMDL);
       _financeiroMDL.CCusto = Convert.ToInt64(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_CC"].ToString());
       _financeiroMDL.NCusto = _conexaoMDL.Ds.Tables[0].Rows[0]["Nome_Centro_Custo"].ToString();
        return _financeiroMDL;
        }
        #endregion

        #region CarregaResponsavel

        public ConexaoMDL CarregaResponsavel(ConexaoMDL conexaoMDL)
        {
            return _financeiroDAL.spc_carrega_responsavel(conexaoMDL);
        }

        #endregion

        #region CarregaCentroCusto

        public ConexaoMDL CarregaCentroCusto(ConexaoMDL conexaoMDL)
        {
            return _financeiroDAL.spc_carrega_centro_custo(conexaoMDL);
        }

        #endregion

        #region CarregaContrato

        public ConexaoMDL CarregaContrato(ConexaoMDL conexaoMDL)
        {
            return _financeiroDAL.spc_carrega_contrato(conexaoMDL);
        }

        #endregion

        #region CarregaDepartamento

        public ConexaoMDL CarregaDepartamento(ConexaoMDL conexaoMDL)
        {
            return _financeiroDAL.spc_carrega_departamento(conexaoMDL);
        }

        #endregion

        #region CadastraConta

        public ConexaoMDL CadastraConta(FinanceiroMDL financeiroMDL)
        {
            return _financeiroDAL.CadastraConta(financeiroMDL);
        }

        #endregion

        #region CarregaTipoDepartamento

        public FinanceiroMDL CarregaTipoDepartamento(FinanceiroMDL financeiroMDL)
        {
            _conexaoMDL = _financeiroDAL.spc_carrega_tipo_departamento(financeiroMDL);

            try
            {
                financeiroMDL.CCusto = Convert.ToInt64(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_CC"]);
            }
            catch
            {
                financeiroMDL.CContrato = Convert.ToInt64(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Contrato"]);
            }

            return financeiroMDL;
        }

        #endregion
    }
}