using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BelaFlor.Lib_Primavera.Model;
using BelaFlor.Lib_Primavera;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using System.IO;



namespace BelaFlor.Controllers
{
    //C:\Program Files\PRIMAVERA\SG800\Dados\LP\ANEXOS

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
                  Request.CreateResponse(HttpStatusCode.NotFound, "O artigo não existe"));
            }
            else
            {
                return artigo;
            }
        }

        public IEnumerable<Article> GetCat(string catid)
        {
            IEnumerable<Article> list = Comercial.ListArticlesCategory(catid);

            //if (cat.Equals("category"))
            //{
                if (list == null)
                {
                    throw new HttpResponseException(
                      Request.CreateResponse(HttpStatusCode.NotFound));
                }
                else return list;
            //}

            //return null;
        }


        //[ActionName("GetImageMethod")]
        public ActionResult GetImage(string imgid)
        {
            //if(arg.Equals("image"))
            //{
                string path = Comercial.GetArtigoImagePath(imgid);

                if (path == null)
                {
                    throw new HttpResponseException(
                      Request.CreateResponse(HttpStatusCode.NotFound));
                }
                else
                {
                    return new FilePathResult(path, "image/jpg");
                }
            //}

            return null;
        }

        /*public byte[] GetImage(string id, string arg)
        {
            if (arg.Equals("image"))
            {
                string path = Comercial.GetArtigoImagePath(id);

                if (path == null)
                {
                    throw new HttpResponseException(
                      Request.CreateResponse(HttpStatusCode.NotFound));
                }
                else
                {
                    FileStream fs = File.OpenRead(path);
                    byte[] bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                    return bytes;
                }
            }

            return null;
        }*/
    }
}
