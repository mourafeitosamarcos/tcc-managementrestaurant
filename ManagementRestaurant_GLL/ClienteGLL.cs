using System;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_GLL
{
    public class ClienteGLL
    {
        private readonly ClienteMDL _clienteMDL = new ClienteMDL();
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();

        #region ValidaCPF

        public bool ValidaCpf(string cpf)
        {
            var multiplicador1 = new[] {10, 9, 8, 7, 6, 5, 4, 3, 2};
            var multiplicador2 = new[] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2};

            switch (cpf)
            {
                case ("000.000.000-00"):
                    return false;
                case ("111.111.111-11"):
                    return false;
                case ("222.222.222-22"):
                    return false;
                case ("333.333.333-33"):
                    return false;
                case ("444.444.444-44"):
                    return false;
                case ("555.555.555-55"):
                    return false;
                case ("666.666.666-66"):
                    return false;
                case ("777.777.777-77"):
                    return false;
                case ("888.888.888-88"):
                    return false;
                case ("999.999.999-99"):
                    return false;
            }

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(Convert.ToString(tempCpf[i]))*multiplicador1[i];

            int resto = soma%11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = Convert.ToString(resto);
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(Convert.ToString(tempCpf[i]))*multiplicador2[i];

            resto = soma%11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + Convert.ToString(resto);
            return cpf.EndsWith(digito);
        }

        #endregion

        #region CarregaDadosCliente

        public ClienteMDL CarregaDadosCliente(ConexaoMDL conexaoMDL)
        {
            _clienteMDL.Nome = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Nome"].ToString();
            _clienteMDL.Telefone = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Telefone"].ToString();

            _clienteMDL.Tipo = conexaoMDL.Ds.Tables[0].Rows[0]["Cli_Tipo"].ToString();

            _clienteMDL.Documento = conexaoMDL.Ds.Tables[0].Rows[0]["Doc_Cliente"].ToString();

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

        #region ValidaCNPJ

        public bool ValidaCnpj(string cnpj)
        {
            var multiplicador1 = new[] {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
            var multiplicador2 = new[] {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2};

            switch (cnpj)
            {
                case ("00.000.000/0000-00"):
                    return false;
                case ("11.111.111/1111-11"):
                    return false;
                case ("22.222.222/2222-22"):
                    return false;
                case ("33.333.333/3333-33"):
                    return false;
                case ("44.444.444/4444-44"):
                    return false;
                case ("55.555.555/5555-55"):
                    return false;
                case ("66.666.666/6666-66"):
                    return false;
                case ("77.777.777/7777-77"):
                    return false;
                case ("88.888.888/8888-88"):
                    return false;
                case ("99.999.999/9999-99"):
                    return false;
            }

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(Convert.ToString(tempCnpj[i]))*multiplicador1[i];

            int resto = (soma%11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = Convert.ToString(resto);
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(Convert.ToString(tempCnpj[i]))*multiplicador2[i];

            resto = (soma%11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + Convert.ToString(resto);
            return cnpj.EndsWith(digito);
        }

        #endregion

        #region PesquisaCEP

        public ClienteMDL PesquisaCep(ClienteMDL clienteMDL)
        {
            _conexaoMDL.Ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" +
                                   clienteMDL.Cep.Replace("-", "").Trim() + "&formato=xml");
            clienteMDL.Rua = _conexaoMDL.Ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim() + ": " +
                             _conexaoMDL.Ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
            clienteMDL.Bairro = _conexaoMDL.Ds.Tables[0].Rows[0]["bairro"].ToString();
            clienteMDL.Cidade = _conexaoMDL.Ds.Tables[0].Rows[0]["cidade"].ToString();
            clienteMDL.Estado = _conexaoMDL.Ds.Tables[0].Rows[0]["uf"].ToString();

            return clienteMDL;
        }

        #endregion

        #region TrataDados

        public ClienteMDL TrataDados(ClienteMDL clienteMDL)
        {
            if (clienteMDL.Telefone != null)
                clienteMDL.Telefone =
                    clienteMDL.Telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();

            if (clienteMDL.Email != null)
                clienteMDL.Email = clienteMDL.Email.ToLower().Trim();

            if (clienteMDL.Documento != null)
                clienteMDL.Documento = clienteMDL.Documento.Replace(".", "").Replace("-", "").Trim();

            if (clienteMDL.Documento != null)
                clienteMDL.Documento = clienteMDL.Documento.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            if (clienteMDL.Cep != null)
                clienteMDL.Cep = clienteMDL.Cep.Replace("-", "").Trim();

            if (clienteMDL.Rua != null)
                clienteMDL.Rua = clienteMDL.Rua.Replace(":", "").Replace(".", "");

            if (clienteMDL.Bairro != null)
                clienteMDL.Bairro = clienteMDL.Bairro.Replace(":", "").Replace(".", "");

            if (clienteMDL.Cidade != null)
                clienteMDL.Cidade = clienteMDL.Cidade.Replace(":", "").Replace(".", "");

            if (clienteMDL.Estado != null)
                clienteMDL.Estado = clienteMDL.Estado.ToUpper();

            return clienteMDL;
        }

        #endregion
    }
}