using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BelaFlor.Lib_Primavera.Model;
using BelaFlor.Lib_Primavera;



namespace FlowerPow.Controllers
{
    public class ArticlesController : ApiController
    {
        //
        // GET: /Articles/

        public IEnumerable<Article> Get()
        {
            return Comercial.ListArticles();
        }


        // GET api/artigo/5    
        public Article Get(string id)
        {
            Article artigo = Comercial.GetArtigo(id);
            if (artigo == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return artigo;
            }
        }

    }
}
