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

namespace BelaFlor.Controllers
{
    public class CategoriesController : ApiController
    {
        //
        // GET: /Categories/

        public IEnumerable<Category> Get()
        {
            return Comercial.ListCategories();
        }


        // GET api/categories/5    
        public Category Get(string id)
        {
            Category cat = Comercial.GetCategory(id);
            if (cat == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound, "O cliente não existe."));

            }
            else
            {
                return cat;
            }
        }

    }
}
