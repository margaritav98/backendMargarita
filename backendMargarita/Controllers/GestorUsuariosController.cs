using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using backendMargarita.Models;
using backendMargarita.Models.WS;

namespace backendMargarita.Controllers
{
    public class GestorUsuariosController : BaseController
    {
        private GestorTareasEntities db = new GestorTareasEntities();
        [HttpGet]
        public Reply GetUsuarios([FromBody] SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            try
            {
                var lst = db.Usuarios.Where(d =>  d.idRol == 1 && d.token == model.token);
                if (lst.Count() > 0)
                {
                    List<UsuarioModel> res = (from d in db.Usuarios
                                         where d.estado == true
                                         select new UsuarioModel
                                         {
                                             id = d.id,
                                             email = d.email,
                                             password = d.password,
                                             token = d.token
                                         }).ToList();
                    oR.data = res;
                    oR.result = 1;
                }
                else
                {
                    oR.message = "No es usuario administrador, no está autorizado para esta operación";
                }
                
            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpPost]
        public Reply AddUsuario([FromBody] UsuarioModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            //Validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }
            try
            {
                var lst = db.Usuarios.Where(d =>  d.idRol == 1 && d.token == model.token);
                if (lst.Count() > 0)
                {
                    Usuarios oUsuario = new Usuarios();
                    oUsuario.email = model.email;
                    oUsuario.password = model.password;
                    oUsuario.estado = model.estado;
                    oUsuario.idRol = model.rol;
                    db.Usuarios.Add(oUsuario);
                    db.SaveChanges();
                    List<UsuarioModel> res = (from d in db.Usuarios
                                         where d.estado == true
                                         select new UsuarioModel
                                         {
                                             id = d.id,
                                             email = d.email,
                                             password = d.password,
                                             token = d.token
                                         }).ToList();
                    oR.data = res;
                    oR.result = 1;
                }
                else
                {
                    oR.message = "No es usuario administrador, no está autorizado para esta operación";
                }
                
            }
            catch
            {
                oR.message = "Ocurrión un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpPost]
        public Reply EditUsuario([FromBody] UsuarioModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.idRol == 1 && d.token == model.token);
                if (lst.Count() > 0)
                {
                    Usuarios oUsuario = db.Usuarios.Find(model.id);
                    oUsuario.id = model.id;
                    oUsuario.email = model.email;
                    oUsuario.password = model.password;
                    oUsuario.estado = model.estado;
                    oUsuario.idRol = model.rol;
                    db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    List<UsuarioModel> res = (from d in db.Usuarios
                                              select new UsuarioModel
                                              {
                                                  id = d.id,
                                                  email = d.email,
                                                  password = d.password,
                                                  estado = d.estado,
                                                  token = d.token,
                                                  rol = d.idRol
                                              }).ToList();
                    oR.data = res;
                    oR.result = 1;
                }
                else
                {
                    oR.message = "No es usuario administrador, no está autorizado para esta operación";
                }

            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpPost]
        public Reply DeleteUsuario([FromBody] UsuarioModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.idRol == 1 && d.token == model.token);
                if (lst.Count() > 0)
                {
                    Usuarios oUsuario = db.Usuarios.Find(model.id);
                    db.Usuarios.Remove(oUsuario);
                    db.SaveChanges();
                    List<UsuarioModel> res = List();
                    oR.data = res;
                    oR.result = 1;
                }
                else
                {
                    oR.message = "No es usuario administrador, no está autorizado para esta operación";
                }

            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        #region HELPERS
        private bool Validate(UsuarioModel model)
        {
            if(model.email == "")
            {
                error = "El email es obligatorio";
                return false;
            }
            if (model.password == "")
            {
                error = "Contraseña requerida";
                return false;
            }
            return true;
        }
        private List<UsuarioModel> List()
        {
            List<UsuarioModel> res = (from d in db.Usuarios
                                      where d.estado == true
                                      select new UsuarioModel
                                      {
                                          id = d.id,
                                          email = d.email,
                                          password = d.password,
                                          token = d.token
                                      }).ToList();
            return res;
        }
        #endregion
    }
}