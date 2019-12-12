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
    public class TareasController : BaseController
    {
        private GestorTareasEntities db = new GestorTareasEntities();

        [HttpGet]
        public Reply GetAllTareas([FromBody] SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token);
                if (lst.Count() > 0)
                {
                    List<TareaJoin> res = (from tareas in db.Tareas
                                           join usuarios in db.Usuarios
                                           on tareas.id equals usuarios.id
                                           select new TareaJoin
                                           {
                                               id = tareas.id,
                                               fechaCreacion = tareas.fechaCreacion,
                                               descripcion = tareas.descripcion,
                                               estado = tareas.estado,
                                               fechaVencimiento = tareas.fechaVencimiento,
                                               autor = usuarios.email
                                           }).ToList();
                    oR.data = res;
                    oR.result = 1;
                }
                else
                {
                    oR.message = "No autenticado";
                }

            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpGet]
        public Reply GetTareasAuth([FromBody] TareaModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token);
                if (lst.Count() > 0)
                {
                    List<TareaJoin> res = (from tareas in db.Tareas
                                           join usuarios in db.Usuarios
                                           on tareas.id equals usuarios.id
                                           where usuarios.token == model.token
                                           select new TareaJoin
                                           {
                                               id = tareas.id,
                                               fechaCreacion = tareas.fechaCreacion,
                                               descripcion = tareas.descripcion,
                                               estado = tareas.estado,
                                               fechaVencimiento = tareas.fechaVencimiento,
                                               autor = usuarios.email
                                           }).ToList();

                    if (res.Count() > 0)
                    {
                        oR.data = res;
                        oR.result = 1;
                    }
                }
            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpGet]
        public Reply GetTareasEstado([FromBody] TareaEstado model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token);
                if (lst.Count() > 0)
                {
                    
                    if (model.estado == true)
                    {
                        List<TareaJoin> res = (from tareas in db.Tareas
                                               join usuarios in db.Usuarios
                                               on tareas.id equals usuarios.id
                                               where tareas.estado == true
                                               select new TareaJoin
                                               {
                                                   id = tareas.id,
                                                   fechaCreacion = tareas.fechaCreacion,
                                                   descripcion = tareas.descripcion,
                                                   estado = tareas.estado,
                                                   fechaVencimiento = tareas.fechaVencimiento,
                                                   autor = usuarios.email
                                               }).ToList();

                        if (res.Count() > 0)
                        {
                            oR.data = res;
                            oR.result = 1;
                        }
                    }
                    if (model.estado == false)
                    {
                        List<TareaJoin> res = (from tareas in db.Tareas
                                               join usuarios in db.Usuarios
                                               on tareas.id equals usuarios.id
                                               where tareas.estado == false
                                               select new TareaJoin
                                               {
                                                   id = tareas.id,
                                                   fechaCreacion = tareas.fechaCreacion,
                                                   descripcion = tareas.descripcion,
                                                   estado = tareas.estado,
                                                   fechaVencimiento = tareas.fechaVencimiento,
                                                   autor = usuarios.email
                                               }).ToList();

                        if (res.Count() > 0)
                        {
                            oR.data = res;
                            oR.result = 1;
                        }
                    }
                    else
                    {
                        List<TareaJoin> res = (from tareas in db.Tareas
                                               join usuarios in db.Usuarios
                                               on tareas.id equals usuarios.id
                                               select new TareaJoin
                                               {
                                                   id = tareas.id,
                                                   fechaCreacion = tareas.fechaCreacion,
                                                   descripcion = tareas.descripcion,
                                                   estado = tareas.estado,
                                                   fechaVencimiento = tareas.fechaVencimiento,
                                                   autor = usuarios.email
                                               }).ToList();

                        if (res.Count() > 0)
                        {
                            oR.data = res;
                            oR.result = 1;
                        }
                    }
                }
            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpGet]
        public Reply GetTareasVencimiento([FromBody] TareaEstado model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token);
                if (lst.Count() > 0)
                {
                    List<TareaJoin> res = (from tareas in db.Tareas
                                           join usuarios in db.Usuarios
                                           on tareas.id equals usuarios.id
                                           orderby tareas.fechaVencimiento ascending
                                           select new TareaJoin
                                           {
                                               id = tareas.id,
                                               fechaCreacion = tareas.fechaCreacion,
                                               descripcion = tareas.descripcion,
                                               estado = tareas.estado,
                                               fechaVencimiento = tareas.fechaVencimiento,
                                               autor = usuarios.email
                                           }).ToList();

                    if (res.Count() > 0)
                    {
                        oR.data = res;
                        oR.result = 1;
                    }
                }
            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        [HttpPost]
        public Reply AddTarea([FromBody] TareaModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token);
                if (lst.Count() > 0)
                {
                    Tareas oTarea = new Tareas();
                    oTarea.fechaCreacion = model.fechaCreacion;
                    oTarea.descripcion = model.descripcion;
                    oTarea.estado = model.estado;
                    oTarea.fechaVencimiento = model.fechaVencimiento;
                    oTarea.idUsuario = model.usuario;
                    db.Tareas.Add(oTarea);
                    db.SaveChanges();
                    List<TareaJoin> res = (from tareas in db.Tareas
                                           join usuarios in db.Usuarios
                                           on tareas.id equals usuarios.id
                                           select new TareaJoin
                                           {
                                               id = tareas.id,
                                               fechaCreacion = tareas.fechaCreacion,
                                               descripcion = tareas.descripcion,
                                               estado = tareas.estado,
                                               fechaVencimiento = tareas.fechaVencimiento,
                                               autor = usuarios.email
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
        public Reply EditTarea([FromBody] TareaModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token);
                if (lst.Count() > 0)
                {
                    Tareas oTarea = db.Tareas.Find(model.id);
                    oTarea.id = model.id;
                    oTarea.fechaCreacion = model.fechaCreacion;
                    oTarea.descripcion = model.descripcion;
                    oTarea.estado = model.estado;
                    oTarea.fechaVencimiento = model.fechaVencimiento;
                    oTarea.idUsuario = model.usuario;
                    db.Entry(oTarea).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    List<TareaJoin> res = (from tareas in db.Tareas
                                           join usuarios in db.Usuarios
                                           on tareas.id equals usuarios.id
                                           select new TareaJoin
                                           {
                                               id = tareas.id,
                                               fechaCreacion = tareas.fechaCreacion,
                                               descripcion = tareas.descripcion,
                                               estado = tareas.estado,
                                               fechaVencimiento = tareas.fechaVencimiento,
                                               autor = usuarios.email
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
        public Reply DeleteTarea([FromBody] TareaModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;
            if (!Verify(model.token))
            {
                oR.message = "No autenticado";
            }
            try
            {
                var lst = db.Usuarios.Where(d => d.token == model.token && d.id == model.usuario);
                if (lst.Count() > 0)
                {
                    Tareas oTarea = db.Tareas.Find(model.id);
                    db.Tareas.Remove(oTarea);
                    db.SaveChanges();
                    //List<TareaJoin> res = List();
                }
                else
                {
                    oR.message = "No está autorizado para esta operación";
                }

            }
            catch
            {
                oR.message = "Ocurrió un error en el servidor, intenta más tarde";
            }
            return oR;
        }

        private List<TareaJoin> List()
        {
            List<TareaJoin> res = (from tareas in db.Tareas
                                   join usuarios in db.Usuarios
                                   on tareas.id equals usuarios.id
                                   select new TareaJoin
                                   {
                                       id = tareas.id,
                                       fechaCreacion = tareas.fechaCreacion,
                                       descripcion = tareas.descripcion,
                                       estado = tareas.estado,
                                       fechaVencimiento = tareas.fechaVencimiento,
                                       autor = usuarios.email
                                   }).ToList();
            return res;
        }
    }
}