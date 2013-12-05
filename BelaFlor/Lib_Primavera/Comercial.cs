using Interop.ErpBS800;
using Interop.GcpBE800;
using Interop.StdBE800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;

namespace BelaFlor.Lib_Primavera
{
    public class Comercial
    {
        # region User

        #endregion User;


        # region Client

        public static List<Model.Client> ListClients()
        {
            ErpBS objMotor = new ErpBS();
            //MotorPrimavera mp = new MotorPrimavera();
            StdBELista objList;

            Model.Client cli = new Model.Client();
            List<Model.Client> listClients = new List<Model.Client>();


            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {
                
                //objList = PriEngine.Engine.Comercial.Clients.LstClients();
       
                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, Morada, Telefone, NumContrib as NumContribuinte FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    cli = new Model.Client();
                    cli.CodCliente = objList.Valor("Cliente");
                    cli.NomeCliente = objList.Valor("Nome");
                    cli.Moeda = objList.Valor("Moeda");
                    cli.NumContribuinte = objList.Valor("NumContribuinte");
                    cli.Moeda = objList.Valor("Morada");
                    cli.NumContribuinte = objList.Valor("Telefone");

                    listClients.Add(cli);
                    objList.Seguinte();

                }

                return listClients;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Client GetClient(string codClient)
        {
            ErpBS objMotor = new ErpBS();

            GcpBECliente objCli = new GcpBECliente();


            Model.Client myCli = new Model.Client();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codClient) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codClient);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.MoradaCliente = objCli.get_Morada();
                    myCli.Telefone = objCli.get_Telefone();
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

        public static Lib_Primavera.Model.RespostaErro UpdClient(Lib_Primavera.Model.Client cliente)
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


        public static Lib_Primavera.Model.RespostaErro DelClient(string codClient)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codClient) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codClient);
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



        public static Lib_Primavera.Model.RespostaErro InsertClientObj(Model.Client cli)
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
        public static void InsereClient(string codClient, string nomeClient, string numContribuinte, string moeda)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();

            GcpBEClient myCli = new GcpBEClient();

            objMotor = mp.AbreEmpresa("DEMO", "", "", "Default");

            myCli.set_Client(codClient);
            myCli.set_Nome(nomeClient);
            myCli.set_NumContribuinte(numContribuinte);
            myCli.set_Moeda(moeda);

            objMotor.Comercial.Clients.Actualiza(myCli);

        }


        */

        
        #endregion Client;   // -----------------------------  END   CLIENTE    ----------------------- 


        # region Article

        public static Lib_Primavera.Model.Article GetArtigo(string codArtigo)
        {
            // ErpBS objMotor = new ErpBS();
            
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Article myArt = new Model.Article();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    StdBELista objList = PriEngine.Engine.Consulta("Select top 1 id from anexos where chave ='" + codArtigo + "' and tabela=4");

                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();
                    myArt.idImagem = objList.Valor("id");

                    return myArt;
                }
            }
            else
            {
                return null;
            }
            
        }

        public static string GetArtigoImagePath(string codArtigo)
        {
            // ErpBS objMotor = new ErpBS();

            GcpBEArtigo objArtigo = new GcpBEArtigo();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    StdBELista objList = PriEngine.Engine.Consulta("Select top 1 id from anexos where chave ='" + codArtigo + "' and tabela=4");

                    return "C:/Program Files/PRIMAVERA/SG800/Dados/LP/ANEXOS/" + objList.Valor("id") + ".jpg";
                }
            }
            else
            {
                return null;
            }

        }


        public static List<Model.Article> ListArticles()
        {
            ErpBS objMotor = new ErpBS();
            //MotorPrimavera mp = new MotorPrimavera();
            StdBELista objList;

            Model.Article art = new Model.Article();
            List<Model.Article> listArts = new List<Model.Article>();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Article();
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

        #endregion Article;


        # region Order

        public static Model.RespostaErro InsertOrder(Model.DocVenda dv)
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
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = "2:"+ex.Message;
                return erro;
            }
        }


        public static List<Model.DocVenda> List_Orders()
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



        public static Model.DocVenda Get_Order(string numdoc)
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

        public static List<Model.DocVenda> Get_Orders_Client(string cod_cliente)
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
        
        # endregion Order;
    }
}