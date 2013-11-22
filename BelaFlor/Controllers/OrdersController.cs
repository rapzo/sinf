using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BelaFlor.Lib_Primavera.Model;
using BelaFlor.Lib_Primavera;


namespace BelaFlor.Controllers
{
    public class OrdersController : ApiController
    {
        //
        // GET: /Orders/

        public IEnumerable<DocVenda> Get()
        {
            return Comercial.List_Orders();
        }

        public DocVenda GetOrder(string numdoc)
        {
            DocVenda docvenda = Comercial.Get_Order(numdoc);
            if (docvenda == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return docvenda;
            }
        }

        public IEnumerable<DocVenda> GetOrderClient(string id)
        {
            IEnumerable<DocVenda> docsvenda = Comercial.Get_Orders_Client(id);

            if (docsvenda == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return docsvenda;
            }
        }

        [ActionName("PostMethod")]
        public HttpResponseMessage Post(DocVenda docvenda)
        {
            RespostaErro erro = new RespostaErro();
            erro = Comercial.InsertOrder(docvenda);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, docvenda);
                string uri = Url.Link("ActionApi", new { NumDoc = docvenda.NumDoc });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else if(erro.Erro == 1)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, erro.Descricao);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

    }
}
