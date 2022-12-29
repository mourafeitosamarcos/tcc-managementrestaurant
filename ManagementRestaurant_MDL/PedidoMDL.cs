using System;
using System.Data;

namespace ManagementRestaurant_MDL
{
    public class PedidoMDL
    {
        public String Tipo { get; set; }
        public String Compra { get; set; }
        public String Cliente { get; set; }
        public Double Valor { get; set; }
        public Double ValorTotal { get; set; }
        public Int32 CPedido { get; set; }
        public DateTime Contrato { get; set; }
        public DateTime FContrato { get; set; }
        public Int32 QDia { get; set; }
        public DateTime Previsao { get; set; }
        public DateTime Reserva { get; set; }
        public DateTime DReserva { get; set; }
        public DateTime HReserva { get; set; }
        public Int32 Mesa { get; set; }
        public DataSet Registro { get; set; }
    }
}