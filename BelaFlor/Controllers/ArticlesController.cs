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
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Windows.Forms;
using System.Web.Mvc;

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
        //public HttpResponseMessage GetArticleImage(string imgid)
        //{
        //    string path = Comercial.GetArtigoImagePath(imgid);

        //    if (path == null)
        //    {
        //        throw new HttpResponseException(
        //            Request.CreateResponse(HttpStatusCode.NotFound));
        //    }
        //    else
        //    {
        //        Image img = Clipboard.GetImage(path, 200, 200);
        //        MemoryStream ms = new MemoryStream();
        //        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //        result.Content = new ByteArrayContent(ms.ToArray());
        //        result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        //        return result;
        //    }
        //}

        public HttpResponseMessage GetArticleImage(string imgid, int width, int height)
        {
            string path = Comercial.GetArtigoImagePath(imgid);

            if (path == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                Image img = Image.FromFile(path);
                img = new Bitmap(img, new Size(width, height));
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return result;
            }
        }

        public HttpResponseMessage GetArticleImage(string imgid)
        {
            string path = Comercial.GetArtigoImagePath(imgid);

            if (path == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                Image img = Image.FromFile(path);
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return result;
            }
        }

        /*public byte[] GetArticleImage(string id, string arg)
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
