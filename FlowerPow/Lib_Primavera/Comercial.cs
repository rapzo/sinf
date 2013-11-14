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
    public class Comercial
    {


        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            ErpBS objMotor = new ErpBS();
            //MotorPrimavera mp = new MotorPrimavera();
            StdBELista objList;

            Model.Cliente cli = new Model.Cliente();
            List<Model.Cliente> listClientes = new List<Model.Cliente>();


            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {
                
                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();
       
                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    cli = new Model.Cliente();
                    cli.CodCliente = objList.Valor("Cliente");
                    cli.NomeCliente = objList.Valor("Nome");
                    cli.Moeda = objList.Valor("Moeda");
                    cli.NumContribuinte = objList.Valor("NumContribuinte");

                    listClientes.Add(cli);
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            ErpBS objMotor = new ErpBS();

            GcpBECliente objCli = new GcpBECliente();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {



            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            ErpBS objMotor = new ErpBS();
             
            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }



        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            //ErpBS objMotor = new ErpBS();
            //MotorPrimavera mp = new MotorPrimavera();
            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        /*
        public static void InsereCliente(string codCliente, string nomeCliente, string numContribuinte, string moeda)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();

            GcpBECliente myCli = new GcpBECliente();

            objMotor = mp.AbreEmpresa("DEMO", "", "", "Default");

            myCli.set_Cliente(codCliente);
            myCli.set_Nome(nomeCliente);
            myCli.set_NumContribuinte(numContribuinte);
            myCli.set_Moeda(moeda);

            objMotor.Comercial.Clientes.Actualiza(myCli);

        }


        */

        
        #endregion Cliente;   // -----------------------------  END   CLIENTE    ----------------------- 


        # region Artigos

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            // ErpBS objMotor = new ErpBS();
            
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }


            }
            else
            {
                return null;
            }
            
        }





        public static List<Model.Artigo> ListaArtigos()
        {
            ErpBS objMotor = new ErpBS();
            //MotorPrimavera mp = new MotorPrimavera();
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;
            
            }

        }

        #endregion Artigos;


        # region Encomendas

        public static Model.RespostaErro InsereEncomenda(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();
            //GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();
            //GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();
            //PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc((DateTime)dv.Data);

                    //EDIT

                    myEnc.set_Entidade((string)dv.Entidade);
                    myEnc.set_Serie((string)dv.Serie);
                    //myEnc.set_Entidade("0001");
                    //myEnc.set_Serie("2013");
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");

                    int y = DateTime.Now.Year;
                    int m = DateTime.Now.Month;
                    int d =DateTime.Now.Day;
                    int h =DateTime.Now.Hour;
                    int min = DateTime.Now.Minute;
                    int s = DateTime.Now.Second;
                    DateTime dt = new DateTime(y,m,d,h,min,s);

                    myEnc.set_DataDoc((DateTime)dt);
                    myEnc.set_DataVenc((DateTime)dt);
                    myEnc.set_Cambio(1);
                    myEnc.set_CambioMBase(1);
                    myEnc.set_CambioMAlt(1);
                    myEnc.set_CondPag("1");
                    myEnc.set_EntidadeFac((string)dv.Entidade);
                    myEnc.set_Seccao("1");
                    myEnc.set_RegimeIva("0");
                    myEnc.set_ArredondamentoIva(0);


                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);

                    for(int i = 0; i<lstlindv.Count; i++)
                    {
                        //{
                            try
                            {
                                //PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, "DV.0001", 2.0, "", "", 14.0, 0.0);
                                PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lstlindv.ElementAt(i).CodArtigo, lstlindv.ElementAt(i).Quantidade, "", "", lstlindv.ElementAt(i).PrecoUnitario, lstlindv.ElementAt(i).Desconto);

                                /*
                                    myLin.set_TipoLinha("20");
                                    myLin.set_Artigo("DV.0001");
                                    myLin.set_Quantidade(2);
                                    myLin.set_Armazem("");
                                    myLin.set_Localizacao("");
                                    myLin.set_PrecUnit(14);
                                    myLin.set_Desconto1(0);
                                    myLin.set_Desconto2(0);
                                    myLin.set_Desconto3(0);
                                    myLin.set_DescontoComercial(0);
                                    myLin.set_Unidade("UN");
                                    myLin.set_DataEntrega(DateTime.Now);
                                    myLinhas.Insere(myLin);
                                    //PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, "DV.0001", 2, "", "", 14, 0);
                                    myEnc.set_Linhas(myLinhas);
                                */
                            }
                            catch (Exception e)
                            {

                                erro.Erro = 1;
                                erro.Descricao = "1:" + lstlindv.Count + e.Message;
                                return erro;
                            }
                        //}
                    }

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc,"Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
            
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";

                    return erro;
                }
            }
            catch (Exception ex)
            {
                //PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = "2:"+ex.Message;
                return erro;
            }
        }


        public static List<Model.DocVenda> List_Encomendas()
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();
            StdBELista objListCab;
            StdBELista objListLin;

            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();

            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            objMotor = mp.AbreEmpresa("BELAFLOR", "sa", "123456", "Default");

            objListCab = objMotor.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");

            while (!objListCab.NoFim())
            {
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");

                objListLin = objMotor.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");

                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
 
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                listdv.Add(dv);
                objListCab.Seguinte();
            }

            return listdv;
        }



        public static Model.DocVenda Get_Encomenda(string numdoc)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();
            StdBELista objListCab;
            StdBELista objListLin;

            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            objMotor = mp.AbreEmpresa("BELAFLOR", "sa", "123456", "Default");

            string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie FROM CabecDoc WHERE TipoDoc='ECL' AND NumDoc='" + numdoc + "'";
            objListCab = objMotor.Consulta(st);

            if (objListCab.Vazia())
                return null;

            dv = new Model.DocVenda();
            dv.id = objListCab.Valor("id");
            dv.Entidade = objListCab.Valor("Entidade");
            dv.NumDoc = objListCab.Valor("NumDoc");
            dv.Data = objListCab.Valor("Data");
            dv.TotalMerc = objListCab.Valor("TotalMerc");
            dv.Serie = objListCab.Valor("Serie");

            objListLin = objMotor.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");

            if (objListLin.Vazia())
                return null;

            listlindv = new List<Model.LinhaDocVenda>();
            while (!objListLin.NoFim())
            {
                lindv = new Model.LinhaDocVenda();
                lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                lindv.CodArtigo = objListLin.Valor("Artigo");
                lindv.DescArtigo = objListLin.Valor("Descricao");
                lindv.Quantidade = objListLin.Valor("Quantidade");
                lindv.Unidade = objListLin.Valor("Unidade");
                lindv.Desconto = objListLin.Valor("Desconto1");
                lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                listlindv.Add(lindv);
                objListLin.Seguinte();
            }
   
            dv.LinhasDoc = listlindv;
            return dv;
        }

        public static List<Model.DocVenda> Get_Encomendas_Cliente(string cod_cliente)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();
            StdBELista objListCab;
            StdBELista objListLin;

            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();

            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            objMotor = mp.AbreEmpresa("BELAFLOR", "sa", "123456", "Default");

            objListCab = objMotor.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' AND Entidade='" + cod_cliente + "'");

            if (objListCab.Vazia())
                return null;

            while (!objListCab.NoFim())
            {
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");

                objListLin = objMotor.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");

                if (objListLin.Vazia())
                    return null;

                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                listdv.Add(dv);
                objListCab.Seguinte();
            }

            return listdv;
        }
        
        # endregion Encomendas;
    }
}