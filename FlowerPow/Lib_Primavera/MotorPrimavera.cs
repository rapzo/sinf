using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;


namespace FirstREST.Lib_Primavera
{
    public class MotorPrimavera
    {

        private static  StdPlatBS Plataforma = new StdPlatBS();
        private static ErpBS MotorLE = new ErpBS();

        public ErpBS AbreEmpresa(string strEmpresa, string strUtilizador, string strPassword, string strInstancia)
        {
            //------------------------------------------------------------------------
            //Open plt
            //------------------------------------------------------------------------

            StdBSConfApl objAplConf = new StdBSConfApl();
            ErpBS objMotor = new ErpBS();

            //[ Open Plt 1ª time ]
            EnumTipoPlataforma objTipoPlataforma = new EnumTipoPlataforma();
            objTipoPlataforma = EnumTipoPlataforma.tpProfissional;

            objAplConf.Instancia = strInstancia;
            objAplConf.AbvtApl = "GCP";
            objAplConf.PwdUtilizador = "";
            objAplConf.Utilizador = "";

            StdBETransaccao objStdTransac = new StdBETransaccao();

            Plataforma.AbrePlataformaEmpresaIntegrador(ref strEmpresa, ref objStdTransac, ref objAplConf, ref objTipoPlataforma);


            bool blnModoPrimario = true;
            objMotor.AbreEmpresaTrabalho(ref objTipoPlataforma, ref strEmpresa, ref strUtilizador, ref strPassword, ref objStdTransac, ref strInstancia, ref blnModoPrimario);

            MotorLE = objMotor;

            return MotorLE;
        }
    }
}