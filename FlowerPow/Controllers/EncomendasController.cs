using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using FirstREST.Lib_Primavera;


namespace FlowerPow.Controllers
{
    public class EncomendasController : ApiController
    {
        //
        // GET: /Encomendas/

        public IEnumerable<DocVenda> Get()
        {
            return Comercial.List_Encomendas();
        }

        public DocVenda GetEncomenda(string numdoc)
        {
            DocVenda docvenda = Comercial.Get_Encomenda(numdoc);
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

        public IEnumerable<DocVenda> GetEncomendaCliente(string cod_cliente)
        {
            IEnumerable<DocVenda> docsvenda = Comercial.Get_Encomendas_Cliente(cod_cliente);

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


        public HttpResponseMessage Post(DocVenda docvenda)
        {
            RespostaErro erro = new RespostaErro();
            erro = Comercial.InsereEncomenda(docvenda);

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
