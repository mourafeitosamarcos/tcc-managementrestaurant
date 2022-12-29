using System;
using System.Data;
using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class EstoqueBLL
    {
        private readonly EstoqueDAL _estoqueDAL = new EstoqueDAL();
        private readonly EstoqueGLL _estoqueGLL = new EstoqueGLL();
        private readonly EstoqueMDL _estoqueMDL = new EstoqueMDL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region CadastraFornecedor

        public ConexaoMDL CadastraFornecedor(EstoqueMDL estoqueMDL)
        {
            estoqueMDL = _estoqueGLL.TrataDados(estoqueMDL);

            return _estoqueDAL.CadastraFornecedor(estoqueMDL);
        }

        #endregion

        #region AlteraFornecedor

        public ConexaoMDL AlteraFornecedor(EstoqueMDL estoqueMDL)
        {
            estoqueMDL = _estoqueGLL.TrataDados(estoqueMDL);

            return _estoqueDAL.AlteraFornecedor(estoqueMDL);
        }

        #endregion

        #region CarregaFornecedor

        public ConexaoMDL CarregaFornecedor(ConexaoMDL conexaoMDL)
        {
            return _estoqueDAL.spc_carrega_nome_fornecedor(_conexaoMDL);
        }

        #endregion

        #region CarregaDadosFornecedor

        public EstoqueMDL CarregaDadosFornecedor(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = _estoqueDAL.CarregaDadosFornecedor(estoqueMDL);

            estoqueMDL.FBairro = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Bairro"].ToString();
            estoqueMDL.FRua = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Rua"].ToString();
            estoqueMDL.FNome = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Nome"].ToString();
            estoqueMDL.FComplemento = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Complemento"].ToString();
            estoqueMDL.FTelefone = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Telefone"].ToString();
            estoqueMDL.FCidade = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Cidade"].ToString();
            estoqueMDL.FEstado = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Estado"].ToString();
            estoqueMDL.FCep = _conexaoMDL.Ds.Tables[0].Rows[0]["For_CEP"].ToString();
            estoqueMDL.FEmail = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Email"].ToString();
            estoqueMDL.FnEstabelecimento = _conexaoMDL.Ds.Tables[0].Rows[0]["For_Num"].ToString();

            return estoqueMDL;
        }

        #endregion

        #region CarregaTipoProduto

        public ConexaoMDL CarregaTipoProduto(ConexaoMDL conexaoMDL)
        {
            return _estoqueDAL.spc_carrega_tipo_produto(_conexaoMDL);
        }

        #endregion

        #region CadastraProduto

        public EstoqueMDL CadastraProduto(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = _estoqueDAL.CadastraProduto(estoqueMDL);

            try
            {
                _estoqueMDL.CProduto = Convert.ToInt32(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Produto"]);

                _estoqueMDL.Validador = _conexaoMDL.Validador;
            }
            catch
            {
                _estoqueMDL.Validador = _conexaoMDL.Validador;
            }

            return _estoqueMDL;
        }

        #endregion

        #region CarregaNomeProduto

        public EstoqueMDL CarregaNomeProduto(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL = _estoqueDAL.spc_carrega_nome_produto(estoqueMDL);

            try
            {
                _estoqueMDL.NProduto = _conexaoMDL.Ds.Tables[0].Rows[0]["Pro_Nome"].ToString();

                _estoqueMDL.Validador = true;
            }
            catch
            {
                _estoqueMDL.Validador = false;
            }

            return _estoqueMDL;
        }

        #endregion

        #region CadastraEntradaEstoque

        public ConexaoMDL CadastraEntradaEstoque(EstoqueMDL estoqueMDL)
        {
            return _estoqueDAL.CadastraEntradaEstoque(estoqueMDL);
        }

        #endregion

        #region CarregaNomeProdutoDropdown

        public ConexaoMDL CarregaNomeProdutoDropdown()
        {
            return _estoqueDAL.spc_carrega_nome_produto_dropdown();
        }

        #endregion

        #region CadastraPrato

        public ConexaoMDL CadastraPrato(EstoqueMDL estoqueMDL, DataSet CadIng)
        {
            return _estoqueDAL.CadastraPrato(estoqueMDL, CadIng);
        }

        #endregion

        #region CarregaNomePratoDropdown

        public ConexaoMDL CarregaNomePratoDropdown()
        {
            return _estoqueDAL.spc_carrega_nome_prato_dropdown();
        }

        #endregion
    }
}