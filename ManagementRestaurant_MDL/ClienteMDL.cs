using System;
using System.Data;

namespace ManagementRestaurant_MDL
{
    public class ClienteMDL
    {
        public String Nome { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }
        public String Documento { get; set; }
        public String Cep { get; set; }
        public String Rua { get; set; }
        public String NEstabelecimento { get; set; }
        public String Bairro { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public DataSet Registro { get; set; }
        public Boolean Validador { get; set; }
        public String Tipo { get; set; }
    }
}