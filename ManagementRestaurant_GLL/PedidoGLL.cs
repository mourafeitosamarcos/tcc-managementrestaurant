using ManagementRestaurant_MDL;

namespace ManagementRestaurant_GLL
{
    public class PedidoGLL
    {
        private readonly ClienteMDL _clienteMDL = new ClienteMDL();

        #region CarregaDadosCliente

        public ClienteMDL CarregaDadosClientePedido(ConexaoMDL conexaoMDL)
        {
            _clienteMDL.Nome = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Nome"].ToString();
            _clienteMDL.Telefone = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Telefone"].ToString();

            _clienteMDL.Tipo = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Tipo"].ToString();
            
            _clienteMDL.Documento = conexaoMDL.Ds.Tables[0].Rows[0]["Numero_pedido"].ToString();

            _clienteMDL.Cep = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Cep"].ToString();
            _clienteMDL.Rua = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Rua"].ToString();
            _clienteMDL.NEstabelecimento = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_NumEstabelecimento"].ToString();
            _clienteMDL.Bairro = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Bairro"].ToString();
            _clienteMDL.Cidade = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Cidade"].ToString();
            _clienteMDL.Estado = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Estado"].ToString();
            _clienteMDL.Registro = conexaoMDL.Ds;
            return _clienteMDL;
        }

        #endregion
    }
}