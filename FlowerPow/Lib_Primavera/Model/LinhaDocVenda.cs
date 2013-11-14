using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstREST.Lib_Primavera.Model
{
    public class LinhaDocVenda
    {
        public dynamic IdCabecDoc { get; set; }

        public dynamic DescArtigo { get; set; }

        public dynamic Unidade { get; set; }

        public double TotalILiquido { get; set; }

        public double TotalLiquido { get; set; }

        public string CodArtigo { get; set; }

        public double Quantidade { get; set; }

        public double Desconto { get; set; }

        public double PrecoUnitario { get; set; }
    }
}
