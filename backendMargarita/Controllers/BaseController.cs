using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backendMargarita.Models;

namespace backendMargarita.Controllers
{
    public class BaseController : ApiController
    {
        private GestorTareasEntities db = new GestorTareasEntities();

        public string error = "";
        public bool Verify(string token)
        {
            var lst = db.Usuarios.Where(d => d.token == token);
            if (lst.Count() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
