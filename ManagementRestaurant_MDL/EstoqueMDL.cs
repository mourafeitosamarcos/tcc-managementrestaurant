using System;

namespace ManagementRestaurant_MDL
{
    public class EstoqueMDL
    {
        public String FNome { get; set; }
        public String FTelefone { get; set; }
        public String FEmail { get; set; }
        public String FCnpj { get; set; }
        public String FCep { get; set; }
        public String FRua { get; set; }
        public String FnEstabelecimento { get; set; }
        public String FBairro { get; set; }
        public String FCidade { get; set; }
        public String FEstado { get; set; }
        public String FComplemento { get; set; }
        public Int32 CProduto { get; set; }
        public String NProduto { get; set; }
        public String Produto { get; set; }
        public String NFiscal { get; set; }
        public DateTime Validade { get; set; }
        public Int32 Lote { get; set; }
        public Int32 Quantidade { get; set; }
        public Int32 Compra { get; set; }
        public Boolean Validador { get; set; }
        public String NPrato { get; set; }
        public Double VPrato { get; set; }
        public String NIngrediente { get; set; }
        public Double QIngrediente { get; set; }
        public Int32 CPrato { get; set; }
    }
}