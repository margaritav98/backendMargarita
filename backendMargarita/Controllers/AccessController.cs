using backendMargarita.Models.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using backendMargarita.Models;

namespace backendMargarita.Controllers
{
    public class AccessController : ApiController
    {
        private GestorTareasEntities db = new GestorTareasEntities();
        
        [HttpPost]
        public Reply Login([FromBody] AccessViewModel model)
        {
            Reply oR = new Reply();
            try
            {
                var lst = db.Usuarios.Where(d => d.email == model.email && d.password == model.password && d.estado == true);
                if (lst.Count() > 0)
                {
                    oR.result = 1;
                    oR.data = Guid.NewGuid().ToString();
                    Usuarios oUser = lst.First();
                    oUser.token = (string)oR.data;
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                } 
                else
                {
                    oR.message = "Datos erróneos";
                }
            }
            catch 
            {
                oR.result = 0;
                oR.message = "Ocurrió un error";
            }
            return oR;
        }
    }
}
