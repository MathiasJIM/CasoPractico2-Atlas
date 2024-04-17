using Abstracciones.DA;
using Abstracciones.Entidad;
using Abstracciones.Modelos;
using Dapper;
using Helpers;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class SeguridadDA : ISeguridadDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public SeguridadDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Abstracciones.Modelos.Login> ObtenerUsuario(Abstracciones.Modelos.Login usuario)
        {
            string sql = @"[GetLoginByEmail]";
            var Consulta = await _sqlConnection.QueryAsync<Abstracciones.Entidad.Login>(sql, new { usuario.Email });
            return Convertidor.Convertir<Abstracciones.Entidad.Login, Abstracciones.Modelos.Login>(Consulta.FirstOrDefault());
        }
    }
}
