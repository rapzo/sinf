using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstREST.Lib_Primavera.Model
{
    public class DocVenda
    {
        public dynamic id { get; set; }

        public dynamic NumDoc { get; set; }

        public dynamic Data { get; set; }

        public dynamic TotalMerc { get; set; }

        public dynamic Entidade { get; set; }

        public dynamic Serie { get; set; }

        public List<LinhaDocVenda> LinhasDoc { get; set; }

    }
}
