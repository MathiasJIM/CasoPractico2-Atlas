using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class AutenticacionBW : IAutenticacionBW
    {

        private IAutenticacionBC _autenticacionBC;
        private ISeguridadDA _seguridadDA;

        public AutenticacionBW(IAutenticacionBC autenticacionBC, ISeguridadDA seguridadDA)
        {
            _autenticacionBC = autenticacionBC;
            _seguridadDA = seguridadDA;
        }

        public Token Login(LoginAutenticar login)
        {
            return _autenticacionBC.Login(login);
        }

        public async Task<Abstracciones.Modelos.Login> ObtenerUsuario(Abstracciones.Modelos.Login usuario)
        {
            return await _seguridadDA.ObtenerUsuario(usuario);
        }
    }
}
