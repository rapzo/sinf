using FirstREST.Lib_Primavera;
using FirstREST.Lib_Primavera.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlowerPow.Controllers
{
    public class SessionsController : ApiController
    {
        // GET api/default1/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/default1
        public void Post([FromBody]string value)
        {
        }

        /*public HttpResponseMessage PostRegisto(Utilizador utilizador)
        {
            RespostaErro erro = new RespostaErro();
            erro = Comercial.RegistaUtilizador(utilizador);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, utilizador);
                string uri = Url.Link("ActionApi", new { CodCliente = utilizador.CodCliente });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }*/

        // PUT api/default1/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/default1/5
        public void Delete(int id)
        {
        }
    }
}
