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
    public class UsersController : ApiController
    {
        //
        // GET: /Users/

        [ActionName("ActivateMethod")]
        public HttpResponseMessage Post(User user)
        {
            int erro = BelaFlor.Lib_Primavera.Model.User.activate(user);
            
            if (erro == 1)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, user);
                string uri = Url.Link("ActionApi", user.Username);
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, "Sem permissoes!");
            }

        }
    }
}
