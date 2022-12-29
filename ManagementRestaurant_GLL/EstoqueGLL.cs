using System;
using ManagementRestaurant_MDL;

namespace ManagementRestaurant_GLL
{
    public class EstoqueGLL
    {
        private readonly ConexaoMDL _conexaoMDL = new ConexaoMDL();

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

        public EstoqueMDL PesquisaCep(EstoqueMDL estoqueMDL)
        {
            _conexaoMDL.Ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" +
                                   estoqueMDL.FCep.Replace("-", "").Trim() + "&formato=xml");
            estoqueMDL.FRua = _conexaoMDL.Ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim() + ": " +
                              _conexaoMDL.Ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
            estoqueMDL.FBairro = _conexaoMDL.Ds.Tables[0].Rows[0]["bairro"].ToString();
            estoqueMDL.FCidade = _conexaoMDL.Ds.Tables[0].Rows[0]["cidade"].ToString();
            estoqueMDL.FEstado = _conexaoMDL.Ds.Tables[0].Rows[0]["uf"].ToString();

            return estoqueMDL;
        }

        #endregion

        #region TrataDados

        public EstoqueMDL TrataDados(EstoqueMDL estoqueMDL)
        {
            estoqueMDL.FTelefone =
                estoqueMDL.FTelefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();

            if (estoqueMDL.FEmail != null)
                estoqueMDL.FEmail = estoqueMDL.FEmail.ToLower().Trim();

            if (estoqueMDL.FCnpj != null)
                estoqueMDL.FCnpj = estoqueMDL.FCnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();

            estoqueMDL.FCep = estoqueMDL.FCep.Replace("-", "").Trim();
            estoqueMDL.FRua = estoqueMDL.FRua.Replace(":", "").Replace(".", "");
            estoqueMDL.FBairro = estoqueMDL.FBairro.Replace(":", "").Replace(".", "");
            estoqueMDL.FCidade = estoqueMDL.FCidade.Replace(":", "").Replace(".", "");
            estoqueMDL.FEstado = estoqueMDL.FEstado.ToUpper();

            return estoqueMDL;
        }

        #endregion
    }
}