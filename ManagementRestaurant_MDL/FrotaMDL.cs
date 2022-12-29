using System;

namespace ManagementRestaurant_MDL
{
    public class FrotaMDL
    {
        public String Placa { get; set; }
        public String Tipo { get; set; }
        public String Marca { get; set; }
        public String Modelo { get; set; }
        public DateTime DEntrada { get; set; }
        public DateTime DSaida { get; set; }
        public Int16 Vaga { get; set; }
        public Int16 Prisma { get; set; }
        public String FunNome { get; set; }
        public Boolean Validador { get; set; }
        public Boolean Validador2 { get; set; }
    }
}