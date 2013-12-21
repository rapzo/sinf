using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BelaFlor.Lib_Primavera.Model
{
    public class Order
    {
        public string Cliente { get; set; }

        public string CodArtigo { get; set; }

        public double Quantidade { get; set; }



        public int NumDoc { get; set; }

        public DateTime Data { get; set; }



        public double TotalMerc { get; set; }

        public string Serie { get; set; }

        public string idCabecDoc { get; set; }

        public string Descricao { get; set; }

        public string Unidade { get; set; }

        public double PrecUnit { get; set; }

        public float Desconto1 { get; set; }

        public double TotalILiquido { get; set; }

        public double PrecoLiquido { get; set; }
    }
}
