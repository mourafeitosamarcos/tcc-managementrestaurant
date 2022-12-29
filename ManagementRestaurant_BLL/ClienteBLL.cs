using ManagementRestaurant_DAL;
using ManagementRestaurant_GLL;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL _clienteDAL = new ClienteDAL();
        private readonly ClienteGLL _clienteGLL = new ClienteGLL();
        private ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region AlteraCliente

        public ConexaoMDL AlteraCliente(ClienteMDL clienteMDL)
        {
            _clienteGLL.TrataDados(clienteMDL);

            return _clienteDAL.AlteraCliente(clienteMDL);
        }

        #endregion

        #region CadastraCliente

        public ConexaoMDL CadastraCliente(ClienteMDL clienteMDL)
        {
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            return _clienteDAL.CadastraCliente(clienteMDL);
        }

        #endregion

        #region PesquisaClientes

        public ConexaoMDL PesquisaClientes(string parametro, string coluna, string tipo)
        {
            return _clienteDAL.PesquisaClientes(parametro, coluna, tipo);
        }

        #endregion

        #region CarregaInformacoesCliente

        public ClienteMDL CarregaInformacoesCliente(ClienteMDL clienteMDL)
        {
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            _conexaoMDL = _clienteDAL.spc_carrega_informacoes_cliente(clienteMDL);

            clienteMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Nome"].ToString();
            clienteMDL.Telefone = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Telefone"].ToString();
            clienteMDL.Tipo = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Tipo"].ToString();
            clienteMDL.Cep = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_CEP"].ToString();
            clienteMDL.Rua = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Rua"].ToString();
            clienteMDL.NEstabelecimento = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_NumEstabelecimento"].ToString();
            clienteMDL.Bairro = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Bairro"].ToString();
            clienteMDL.Cidade = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Cidade"].ToString();
            clienteMDL.Estado = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Estado"].ToString();
            clienteMDL.Documento = _conexaoMDL.Ds.Tables[0].Rows[0]["Doc_Cliente"].ToString();
            clienteMDL.Registro = _conexaoMDL.Ds;
            clienteMDL.Validador = _conexaoMDL.Validador;

            return clienteMDL;
        }

        #endregion

        #region CarregaInformacoesClienteReserva

        public ClienteMDL CarregaInformacoesClienteReserva(ClienteMDL clienteMDL)
        {
            clienteMDL = _clienteGLL.TrataDados(clienteMDL);

            _conexaoMDL = _clienteDAL.spc_carrega_informacoes_cliente_reserva(clienteMDL);

            try
            {
                clienteMDL.Nome = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Nome"].ToString();
                clienteMDL.Telefone = _conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Telefone"].ToString();
            }
            catch
            {
                clienteMDL.Validador = false;

                return clienteMDL;
            }

            clienteMDL.Validador = true;

            return clienteMDL;
        }

        #endregion
    }
}