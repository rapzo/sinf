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

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, Fac_Mor, Fac_Tel, NumContrib as NumContribuinte, CDU_CampoVar1 as Username, CDU_CampoVar2 as Password, EnderecoWeb as email FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    cli = new Model.Client();
                    cli.CodCliente = objList.Valor("Cliente");
                    cli.NomeCliente = objList.Valor("Nome");
                    cli.Moeda = objList.Valor("Moeda");
                    cli.NumContribuinte = objList.Valor("NumContribuinte");
                    cli.MoradaCliente = objList.Valor("Fac_Mor");
                    cli.Telefone = objList.Valor("Fac_Tel");
                    cli.Username = objList.Valor("Username");
                    cli.Password = objList.Valor("Password");
                    cli.email = objList.Valor("email");

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

                    if (objCli.get_CamposUtil() != null)
                    {
                        if (objCli.get_CamposUtil().get_Item("CDU_CampoVar1") != null)
                            myCli.Username = objCli.get_CamposUtil().get_Item("CDU_CampoVar1").Valor;
                        if (objCli.get_CamposUtil().get_Item("CDU_CampoVar1") != null)
                            myCli.Password = objCli.get_CamposUtil().get_Item("CDU_CampoVar2").Valor;
                    }
                    myCli.email = objCli.get_EnderecoWeb();
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
                        objCli.set_Telefone(cliente.Telefone);
                        objCli.set_Morada(cliente.MoradaCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);
                        objCli.set_EnderecoWeb(cliente.email);

                        StdBECampo username = new StdBECampo();
                        username.Nome = "CDU_CampoVar1";
                        username.Valor = cliente.Username;

                        StdBECampo password = new StdBECampo();
                        password.Nome = "CDU_CampoVar2";
                        password.Valor = cliente.Password;

                        StdBECampos campos = new StdBECampos();
                        campos.Insere(username);
                        campos.Insere(password);

                        objCli.set_CamposUtil(campos);

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
                    if (cli == null)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "Dados errados!";
                        return erro;
                    }

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);
                    myCli.set_Morada(cli.MoradaCliente);
                    myCli.set_Telefone(cli.Telefone);
                    myCli.set_EnderecoWeb(cli.email);

                    StdBECampo username = new StdBECampo();
                    username.Nome = "CDU_CampoVar1";
                    username.Valor = cli.Username;

                    StdBECampo password = new StdBECampo();
                    password.Nome = "CDU_CampoVar2";
                    password.Valor = cli.Password;

                    StdBECampos campos = new StdBECampos();
                    campos.Insere(username);
                    campos.Insere(password);

                    myCli.set_CamposUtil(campos);

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

            StdBELista objList, objList2;
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
                    objList = PriEngine.Engine.Consulta("Select top 1 id from anexos where chave ='" + codArtigo + "' and tabela=4");

                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();
                    myArt.Obs = objArtigo.get_Observacoes();
                    myArt.Familia = objArtigo.get_Familia();
                    myArt.idImagem = objList.Valor("id");

                    objList2 = PriEngine.Engine.Consulta("Select unidade, pvp1 from artigomoeda where artigo ='" + codArtigo + "'");
                    myArt.Preco = objList2.Valor("pvp1");
                    myArt.Unidade = objList2.Valor("unidade");

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
            StdBELista objList, objList2, objList3, objList4;

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

                    objList2 = PriEngine.Engine.Consulta("Select unidade, pvp1 from artigomoeda where artigo ='" + art.CodArtigo + "'");
                    art.Preco = objList2.Valor("pvp1");
                    art.Unidade = objList2.Valor("unidade");

                    objList3 = PriEngine.Engine.Consulta("Select top 1 id from anexos where chave ='" + art.CodArtigo + "' and tabela=4");
                    if(!objList3.Vazia())
                        art.idImagem = objList3.Valor("id");

                    objList4 = PriEngine.Engine.Consulta("Select observacoes as obs, familia from artigo where artigo ='" + art.CodArtigo + "'");
                    art.Obs = objList4.Valor("obs");
                    art.Familia = objList4.Valor("familia");

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

        public static List<Model.Article> ListArticlesCategory(string cat)
        {
            ErpBS objMotor = new ErpBS();
            //MotorPrimavera mp = new MotorPrimavera();
            StdBELista objList, objList2, objList3, objList4, objList5;

            Model.Article art = new Model.Article();
            List<Model.Article> listArts = new List<Model.Article>();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    string artid = objList.Valor("artigo");
                    objList5 = PriEngine.Engine.Consulta("Select familia from artigo where artigo ='" + artid + "'");

                    if (objList5.Valor("familia") == cat)
                    {
                        art = new Model.Article();
                        art.CodArtigo = objList.Valor("artigo");
                        art.DescArtigo = objList.Valor("descricao");

                        objList2 = PriEngine.Engine.Consulta("Select unidade, pvp1 from artigomoeda where artigo ='" + art.CodArtigo + "'");
                        art.Preco = objList2.Valor("pvp1");
                        art.Unidade = objList2.Valor("unidade");

                        objList3 = PriEngine.Engine.Consulta("Select top 1 id from anexos where chave ='" + art.CodArtigo + "' and tabela=4");
                        if (!objList3.Vazia())
                            art.idImagem = objList3.Valor("id");

                        objList4 = PriEngine.Engine.Consulta("Select observacoes as obs, familia from artigo where artigo ='" + art.CodArtigo + "'");
                        art.Obs = objList4.Valor("obs");
                        art.Familia = objList4.Valor("familia");

                        listArts.Add(art);
                    }
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


        # region Category

        public static Lib_Primavera.Model.Category GetCategory(string id)
        {
            // ErpBS objMotor = new ErpBS();
            GcpBEFamilia objFamilia = new GcpBEFamilia();
            Model.Category myCat = new Model.Category();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                if (PriEngine.Engine.Comercial.Familias.Existe(id) == false)
                {
                    return null;
                }
                else
                {
                    objFamilia = PriEngine.Engine.Comercial.Familias.Edita(id);
                    myCat.CodFamilia = objFamilia.get_Familia();
                    myCat.CodFamilia = objFamilia.get_Descricao();
                   
                    return myCat;
                }
            }
            else
            {
                return null;
            }

        }


        public static List<Model.Category> ListCategories()
        {
            ErpBS objMotor = new ErpBS();
            StdBELista objList;

            Model.Category cat = new Model.Category();
            List<Model.Category> listCats = new List<Model.Category>();

            if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
            {

                objList = PriEngine.Engine.Consulta("Select familia, descricao from familias");

                while (!objList.NoFim())
                {
                    cat = new Model.Category();
                    cat.CodFamilia = objList.Valor("familia");
                    cat.DescFamilia = objList.Valor("descricao");

                    listCats.Add(cat);
                    objList.Seguinte();
                }

                return listCats;

            }
            else
            {
                return null;

            }

        }

        #endregion Category;


        # region Order

        public static Model.RespostaErro InsertOrder(Model.Order dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            try
            {
                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {
                    myEnc.set_Entidade((string)dv.Cliente);
                    myEnc.set_Serie("2013");
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
                    myEnc.set_EntidadeFac((string)dv.Cliente);
                    myEnc.set_Seccao("1");
                    myEnc.set_RegimeIva("0");
                    myEnc.set_ArredondamentoIva(0);

                    StdBELista objList = PriEngine.Engine.Consulta("Select pvp1 from artigomoeda where artigo ='" + dv.CodArtigo + "'");
                    double price = objList.Valor("pvp1");

                    try
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, dv.CodArtigo, dv.Quantidade, "", "", price, 0.0);
                    }
                    catch (Exception e)
                    {

                        erro.Erro = 1;
                        erro.Descricao = "Erro ao inserir linha ao documento:" + e.Message;
                        return erro;
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

        public static List<Model.Order> Get_Orders_Client(string cod_cliente)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();
            StdBELista objListCab;
            StdBELista objListLin;

            Model.Order dv = new Model.Order();
            List<Model.Order> listdv = new List<Model.Order>();

            objMotor = mp.AbreEmpresa("BELAFLOR", "sa", "123456", "Default");

            objListCab = objMotor.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' AND Entidade='" + cod_cliente + "'");

            if (objListCab.Vazia())
                return null;

            while (!objListCab.NoFim())
            {
                dv = new Model.Order();
                dynamic id = objListCab.Valor("id");
                dv.Cliente = objListCab.Valor("Entidade");
                dv.Data = objListCab.Valor("Data");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");

                objListLin = objMotor.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + id + "' order By NumLinha");

                if (objListLin.Vazia())
                    return null;

                dv.idCabecDoc = objListLin.Valor("idCabecDoc");                 
                dv.CodArtigo = objListLin.Valor("Artigo");
                dv.Descricao = objListLin.Valor("Descricao");
                dv.Quantidade = objListLin.Valor("Quantidade");
                dv.Unidade = objListLin.Valor("Unidade");
                dv.PrecUnit = objListLin.Valor("PrecUnit");
                dv.Desconto1 = objListLin.Valor("Desconto1");
                dv.TotalILiquido = objListLin.Valor("TotalILiquido");
                dv.PrecoLiquido = objListLin.Valor("PrecoLiquido");

                listdv.Add(dv);
                objListCab.Seguinte();
            }

            return listdv;
        }

        public static List<Model.Order> List_Orders()
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();
            StdBELista objListCab;
            StdBELista objListLin;

            Model.Order dv = new Model.Order();
            List<Model.Order> listdv = new List<Model.Order>();

            objMotor = mp.AbreEmpresa("BELAFLOR", "sa", "123456", "Default");

            objListCab = objMotor.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");

            if (objListCab.Vazia())
                return null;

            while (!objListCab.NoFim())
            {
                dv = new Model.Order();
                dynamic id = objListCab.Valor("id");
                dv.Cliente = objListCab.Valor("Entidade");
                dv.Data = objListCab.Valor("Data");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");

                objListLin = objMotor.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + id + "' order By NumLinha");

                if (objListLin.Vazia())
                    return null;

                dv.idCabecDoc = objListLin.Valor("idCabecDoc");
                dv.CodArtigo = objListLin.Valor("Artigo");
                dv.Descricao = objListLin.Valor("Descricao");
                dv.Quantidade = objListLin.Valor("Quantidade");
                dv.Unidade = objListLin.Valor("Unidade");
                dv.PrecUnit = objListLin.Valor("PrecUnit");
                dv.Desconto1 = objListLin.Valor("Desconto1");
                dv.TotalILiquido = objListLin.Valor("TotalILiquido");
                dv.PrecoLiquido = objListLin.Valor("PrecoLiquido");

                listdv.Add(dv);
                objListCab.Seguinte();
            }

            return listdv;
        }
        
        # endregion Order;
    }
}