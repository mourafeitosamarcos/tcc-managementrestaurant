using System;
using System.Data;

namespace ManagementRestaurant_MDL
{
    public class FuncionarioMDL
    {
        public String Nome { get; set; }
        public String Telefone { get; set; }
        public String Rg { get; set; }
        public String Cpf { get; set; }
        public String Cnh { get; set; }
        public String CTrabalho { get; set; }
        public String CTrabalho2 { get; set; }
        public String Cep { get; set; }
        public String Rua { get; set; }
        public String NEstabelecimento { get; set; }
        public String Bairro { get; set; }
        public String Cidade { get; set; }
        public String Estado { get; set; }
        public Int16 NAcesso { get; set; }
        public DateTime HEntrada { get; set; }
        public DateTime HSaida { get; set; }
        public DateTime Almoco { get; set; }
        public DateTime FAlmoco { get; set; }
        public String Cargo { get; set; }
        public Double SHora { get; set; }
        public DateTime DAdmissao { get; set; }
        public Double Proventos { get; set; }
        public DateTime Ferias { get; set; }
        public DateTime FFerias { get; set; }
        public Int16 DUteis { get; set; }
        public String Senha { get; set; }
        public String CepSenha { get; set; }
        public Boolean Validador { get; set; }
        public Int16 Status { get; set; }
        public DataSet Registro { get; set; }
        public String Email { get; set; }
        public Int16 Assunto { get; set; }
        public String Mensagem { get; set; }
    }
}