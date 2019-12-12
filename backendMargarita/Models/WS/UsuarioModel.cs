using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendMargarita.Models.WS
{
    public class UsuarioModel : SecurityViewModel
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool estado { get; set; }
        public int rol { get; set; }
    }
}