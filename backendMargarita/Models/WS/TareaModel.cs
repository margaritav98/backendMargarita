using backendMargarita.Models.WS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backendMargarita.Models
{
    public class TareaModel : SecurityViewModel
    {
        public int id { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int usuario { get; set; }
    }
}