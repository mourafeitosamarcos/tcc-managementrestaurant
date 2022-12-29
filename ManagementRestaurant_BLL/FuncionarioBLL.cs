using System;
using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class FuncionarioBLL
    {
        private readonly FuncionarioDAL _funcionarioDAL = new FuncionarioDAL();
        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();

        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region CarregaFuncionario

        public ConexaoMDL CarregaFuncionario(FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);
            _funcionarioGLL.GeraSenha(funcionarioMDL);

            return _funcionarioDAL.CarregaFuncionario(funcionarioMDL);
        }

        #endregion

        #region CadastraFuncionario

        public ConexaoMDL CadastraFuncionario(FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);
            funcionarioMDL = _funcionarioGLL.GeraSenhaCep(funcionarioMDL);

            return _funcionarioDAL.CadastraFuncionario(funcionarioMDL);
        }

        #endregion

        #region AtualizaSenha

        public ConexaoMDL AtualizaSenha(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioGLL.GeraSenha(funcionarioMDL);

            return _funcionarioDAL.spc_atualiza_senha(funcionarioMDL);
        }

        #endregion

        #region ValidaPeriodoFerias

        public ConexaoMDL ValidaPeriodoFerias(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.ValidaPeriodoFerias(_funcionarioMDL);
        }

        #endregion

        #region AplicaFerias

        public ConexaoMDL AplicaFerias(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.AplicaFerias(_funcionarioMDL);
        }

        #endregion

        #region CarregaInformacoesFuncionario

        public FuncionarioMDL CarregaInformacoesFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            _conexaoMDL = _funcionarioDAL.CarregaInformacoesFuncionario(_funcionarioMDL);

            try
            {
                funcionarioMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();
                funcionarioMDL.Telefone = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Telefone"].ToString();
                funcionarioMDL.Rg = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_RG"].ToString();
                funcionarioMDL.Cpf = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();
                funcionarioMDL.Cnh = _conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString();
                funcionarioMDL.DAdmissao = Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["Admissao_Data"]);
                funcionarioMDL.CTrabalho = _conexaoMDL.Ds.Tables[0].Rows[0]["Carteira_de_Trabalho"].ToString();
                funcionarioMDL.Cep = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CEP"].ToString();
                funcionarioMDL.Rua = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Rua"].ToString();
                funcionarioMDL.NEstabelecimento = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_NumEstabelecimento"].ToString();
                funcionarioMDL.Bairro = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Bairro"].ToString();
                funcionarioMDL.Cidade = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Cidade"].ToString();
                funcionarioMDL.Estado = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Estado"].ToString();

                funcionarioMDL.Cargo = _conexaoMDL.Ds.Tables[0].Rows[0]["Cargo_Nome"].ToString();
                funcionarioMDL.SHora = Convert.ToDouble(_conexaoMDL.Ds.Tables[0].Rows[0]["SalarioHora"].ToString());
                funcionarioMDL.NAcesso = Convert.ToInt16(_conexaoMDL.Ds.Tables[0].Rows[0]["Nivel_Acesso"]);

                funcionarioMDL.Registro = _conexaoMDL.Ds;

                funcionarioMDL.Validador = _conexaoMDL.Validador;
            }
            catch
            {
                funcionarioMDL.Validador = _conexaoMDL.Validador;
            }

            return funcionarioMDL;
        }

        #endregion

        #region CarregaNomeCargo

        public ConexaoMDL CarregaNomeCargo(ConexaoMDL conexaoMDL)
        {
            return _funcionarioDAL.spc_carrega_nome_cargo(_conexaoMDL);
        }

        #endregion

        #region CarregaInformacoesPonto

        public FuncionarioMDL CarregaInformacoesPonto(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            _conexaoMDL = _funcionarioDAL.CarregaInformacoesPonto(_funcionarioMDL);

            try
            {
                funcionarioMDL.HEntrada =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["Ponto_Data_Entrada"].ToString());
                funcionarioMDL.Almoco =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["Ponto_Almoco_Entrada"].ToString());
                funcionarioMDL.FAlmoco =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["Ponto_Almoco_Saida"].ToString());
                funcionarioMDL.HSaida =
                    Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["Ponto_Data_Saida"].ToString());

                funcionarioMDL.Validador = _conexaoMDL.Validador;
            }
            catch
            {
                funcionarioMDL.Validador = _conexaoMDL.Validador;
            }

            return funcionarioMDL;
        }

        #endregion

        #region CadastraPontoEntrada

        public ConexaoMDL CadastraPontoEntrada(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.spc_cadastra_ponto_entrada(_funcionarioMDL);
        }

        #endregion

        #region CadastraPontoSaida

        public ConexaoMDL CadastraPontoSaida(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.spc_cadastra_ponto_saida(_funcionarioMDL);
        }

        #endregion

        #region CadastraAlmocoEntrada

        public ConexaoMDL CadastraAlmocoEntrada(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.spc_cadastra_almoco_entrada(_funcionarioMDL);
        }

        #endregion

        #region CadastraAlmocoSaida

        public ConexaoMDL CadastraAlmocoSaida(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.spc_cadastra_almoco_saida(_funcionarioMDL);
        }

        #endregion

        #region AlteraFuncionario

        public ConexaoMDL AlteraFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.AlteraFuncionario(funcionarioMDL);
        }

        #endregion

        #region DesabilitaFuncionario

        public ConexaoMDL DesabilitaFuncionario(FuncionarioMDL funcionarioMDL)
        {
            return _funcionarioDAL.DesabilitaFuncionario(funcionarioMDL);
        }

        #endregion

        #region HabilitaFuncionario

        public ConexaoMDL HabilitaFuncionario(FuncionarioMDL funcionarioMDL)
        {
            return _funcionarioDAL.HabilitaFuncionario(funcionarioMDL);
        }

        #endregion

        #region PesquisaFuncionarios

        public ConexaoMDL PesquisaFuncionarios(string parametro, string coluna)
        {
            return _funcionarioDAL.PesquisaFuncionarios(parametro, coluna);
        }

        #endregion

        #region PesquisaInativos

        public ConexaoMDL PesquisaInativos(string parametro, string coluna)
        {
            return _funcionarioDAL.PesquisaInativos(parametro, coluna);
        }

        #endregion

        #region RegistraLog

        public void RegistraLog(ConexaoMDL conexaoMDL, int i, string j)
        {
            _funcionarioMDL.Cpf = conexaoMDL.Ds.Tables[0].Rows[0]["Fun_CPF"].ToString();

            _funcionarioDAL.spc_registra_log(_funcionarioMDL, i, j);
        }

        #endregion

        #region ValidaEmail

        public FuncionarioMDL ValidaEmail(FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.ValidaEmail(funcionarioMDL);
        }

        #endregion

        #region CadastraFeedback

        public ConexaoMDL CadastraFeedback(FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _funcionarioDAL.spc_cadastra_feedback(funcionarioMDL);
        }

        #endregion
    }
}