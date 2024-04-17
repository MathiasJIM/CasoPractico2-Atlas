using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidad
{
    public class Login
    {
        public Guid Id { get; set; }
        public Guid? IdRol { get; set; }

        public string Nombre { get; set; }
        public string Email { get; set; }

        public string Clave { get; set; }

        public bool? Activo { get; set; }
    }
}
