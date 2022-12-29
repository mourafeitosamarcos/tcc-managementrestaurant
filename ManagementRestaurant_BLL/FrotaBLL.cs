using System;
using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class FrotaBLL
    {
        private readonly FrotaDAL _frotaDAL = new FrotaDAL();

        private readonly FuncionarioGLL _funcionarioGLL = new FuncionarioGLL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();
        private FuncionarioMDL _funcionarioMDL = new FuncionarioMDL();

        #region AlteraVeiculo

        public ConexaoMDL AlteraVeiculo(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);
            return _frotaDAL.AlteraVeiculo(frotaMDL, funcionarioMDL);
        }

        #endregion

        #region CarregaInformacoesFuncionario

        public FuncionarioMDL CarregaInformacoesFuncionario(FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            _conexaoMDL = _frotaDAL.CarregaInformacoesFuncionarioVeiculo(null, _funcionarioMDL);

            try
            {
                funcionarioMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();
                funcionarioMDL.Telefone = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Telefone"].ToString();
                funcionarioMDL.Cnh = _conexaoMDL.Ds.Tables[0].Rows[0]["CNH"].ToString();

                funcionarioMDL.Validador = _conexaoMDL.Validador;
            }
            catch
            {
                funcionarioMDL.Validador = _conexaoMDL.Validador;
            }

            return funcionarioMDL;
        }

        #endregion

        #region CarregaPlacaVeiculoInterno

        public FrotaMDL CarregaPlacaVeiculoInterno(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = _frotaDAL.CarregaPlacaVeiculoInterno(frotaMDL, funcionarioMDL);

            try
            {
                frotaMDL.FunNome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();
                frotaMDL.Tipo = _conexaoMDL.Ds.Tables[0].Rows[0]["Veiculo_Tipo_Interno"].ToString();
                frotaMDL.Marca = _conexaoMDL.Ds.Tables[0].Rows[0]["Marca"].ToString();
                frotaMDL.Modelo = _conexaoMDL.Ds.Tables[0].Rows[0]["Modelo_Internos"].ToString();

                frotaMDL.Validador = true;
            }
            catch
            {
                frotaMDL.Validador = false;
            }

            return frotaMDL;
        }

        #endregion

        #region CarregaVagaPrisma

        public FrotaMDL CarregaVagaPrisma(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _conexaoMDL = _frotaDAL.CarregaVagaPrisma(frotaMDL, funcionarioMDL);

            if (frotaMDL.Validador)
            {
                try
                {
                    _conexaoMDL.Ds.Clear();

                    _conexaoMDL = _frotaDAL.spc_carrega_veiculo_interno(frotaMDL, funcionarioMDL);

                    frotaMDL.Placa = _conexaoMDL.Ds.Tables[0].Rows[0]["Placa"].ToString();
                    frotaMDL.FunNome = _conexaoMDL.Ds.Tables[0].Rows[0]["Fun_Nome"].ToString();
                    frotaMDL.Tipo = _conexaoMDL.Ds.Tables[0].Rows[0]["Veiculo_Tipo_Interno"].ToString();
                    frotaMDL.Marca = _conexaoMDL.Ds.Tables[0].Rows[0]["Marca"].ToString();
                    frotaMDL.Modelo = _conexaoMDL.Ds.Tables[0].Rows[0]["Modelo_Internos"].ToString();

                    frotaMDL.Validador2 = true;
                }
                catch
                {
                    frotaMDL.Validador2 = false;
                }
            }

            return frotaMDL;
        }

        #endregion

        #region CarregaEntradaVeiculo

        public FrotaMDL CarregaEntradaVeiculo(FrotaMDL frotaMDL)
        {
            _conexaoMDL = _frotaDAL.spc_carrega_entrada_veiculo(frotaMDL);

            try
            {
                frotaMDL.Prisma = Convert.ToInt16(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Prisma"]);
                frotaMDL.DEntrada = Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["VData_Entrada"]);
                frotaMDL.DSaida = Convert.ToDateTime(_conexaoMDL.Ds.Tables[0].Rows[0]["VData_Saida"]);
            }
            catch (Exception)
            {
                return frotaMDL;
            }

            return frotaMDL;
        }

        #endregion

        #region CadastraVeiculo

        public ConexaoMDL CadastraVeiculo(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            _funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            return _frotaDAL.CadastraVeiculo(frotaMDL, _funcionarioMDL);
        }

        #endregion

        #region CarregaVeiculoFuncionario

        public FrotaMDL CarregaVeiculoFuncionario(FrotaMDL frotaMDL, FuncionarioMDL funcionarioMDL)
        {
            funcionarioMDL = _funcionarioGLL.TrataDados(funcionarioMDL);

            _conexaoMDL = _frotaDAL.spc_carrega_veiculo_interno(frotaMDL, funcionarioMDL);

            frotaMDL.Marca = _conexaoMDL.Ds.Tables[0].Rows[1]["Marca"].ToString();
            frotaMDL.Modelo = _conexaoMDL.Ds.Tables[0].Rows[1]["Modelo_Internos"].ToString();
            frotaMDL.Placa = _conexaoMDL.Ds.Tables[0].Rows[1]["Placa"].ToString();
            frotaMDL.Tipo = _conexaoMDL.Ds.Tables[0].Rows[1]["Veiculo_Tipo_Interno"].ToString();

            return frotaMDL;
        }

        #endregion

        #region CadastraHoraEntradaInternos

        public ConexaoMDL CadastraHoraEntradaInternos(FrotaMDL frotaMDL)
        {
            return _frotaDAL.spc_cadastra_entrada_veiculo(frotaMDL);
        }

        #endregion

        #region CadastraHoraSaidaInternos

        public ConexaoMDL CadastraHoraSaidaInternos(FrotaMDL frotaMDL)
        {
            return _frotaDAL.spc_cadastra_saida_veiculo(frotaMDL);
        }

        #endregion

        #region CadastraHoraEntradaExternos

        public ConexaoMDL CadastraHoraEntradaExternos(FrotaMDL frotaMDL)
        {
            return _frotaDAL.spc_cadastra_entrada_veiculo(frotaMDL);
        }

        #endregion

        #region CadastraHoraSaidaExternos

        public ConexaoMDL CadastraHoraSaidaExternos(FrotaMDL frotaMDL)
        {
            return _frotaDAL.spc_cadastra_saida_veiculo(frotaMDL);
        }

        #endregion

        #region CarregaPrisma

        public FrotaMDL CarregaPrisma(FrotaMDL frotaMDL)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = _frotaDAL.spc_carrega_prisma();

            frotaMDL.Prisma = Convert.ToInt16(_conexaoMDL.Ds.Tables[0].Rows[0]["Cod_Prisma"].ToString());

            return frotaMDL;
        }

        #endregion

        #region CarregaVagas

        public FrotaMDL CarregaVagas(FrotaMDL frotaMDL)
        {
            _conexaoMDL.Ds.Clear();

            _conexaoMDL = _frotaDAL.spc_carrega_vagas();

            frotaMDL.Vaga = Convert.ToInt16(_conexaoMDL.Ds.Tables[0].Rows.Count);

            return frotaMDL;
        }

        #endregion
    }
}