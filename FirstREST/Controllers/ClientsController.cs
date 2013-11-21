using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using FirstREST.Lib_Primavera;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json.Linq;

namespace FlowerPow.Controllers
{
    public class ClientsController : ApiController
    {
        //
        // GET: /Clients/

        public IEnumerable<Client> Get()
        {
            return Comercial.ListClients();
        }


        // GET api/cliente/5    
        public Client Get(string id)
        {
           Client client = Comercial.GetClient(id);
            if (client == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return client;
            }
        }

        [ActionName("PostMethod")]
        public HttpResponseMessage Post(Client client)
        {
            RespostaErro erro = new RespostaErro();
            erro = Comercial.InsertClientObj(client);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, client);
                string uri = Url.Link("ActionApi", new { CodCliente = client.CodCliente });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

        [ActionName("PutMethod")]
        public HttpResponseMessage Put(Client client)
        {

            RespostaErro erro = new RespostaErro();

            try
            {
                erro = Comercial.UpdClient(client);
                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }



        public HttpResponseMessage Delete(string id)
        {


            RespostaErro erro = new RespostaErro();

            try
            {

                erro = Comercial.DelClient(id);

                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }

            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);

            }

        }


    }
}
