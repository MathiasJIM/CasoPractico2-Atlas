using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface ISeguridadDA
    {
        Task<Abstracciones.Modelos.Login> ObtenerUsuario(Abstracciones.Modelos.Login usuario);
    }
}
