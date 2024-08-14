using clase08.Model;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System.Data;

namespace clase08.Service
{

    public interface IUsuarioService
    {
        Task addUsuario(Usuario usuario);
        string getNombre(Usuario usuario);
        Task update(int UsuarioPK, Usuario usuario);
        Task delete(int UsuarioPK);
        Task<IEnumerable<Usuario>> getAll();
        Task<Usuario> getByPK(int UsuarioPK);

    }
    public class UsuarioService : IUsuarioService
    {
        public string getNombre(Usuario usuario)
        {
            return usuario.Nombre!;
        }
        private readonly IDbConnection _db;

        public UsuarioService(IConfiguration configuration)
        {
            _db = new MySqlConnection(configuration.GetConnectionString("SqlConnection"));
        }


        public void Dispose()
        {
            _db.Close();
        }

        
        public async Task addUsuario(Usuario usuario)
        {
            var sql = """
            INSERT INTO usuario (Rut, Nombre, Edad) VALUES (@Rut, @Nombre, @Edad)
            """;
            await _db.ExecuteAsync(sql, usuario);

        }

        public async Task update(int UsuarioPK, Usuario usuario)
        {
            var sql = """SELECT * FROM usuario WHERE UsuarioPK = @UsuarioPK """;
            var user = await _db.QuerySingleOrDefaultAsync<Usuario>(sql, new { UsuarioPK });

            if (user == null)
                throw new KeyNotFoundException("Usuario no existe");

            sql = """UPDATE usuario SET Rut = @Rut, Nombre = @Nombre, Edad = @Edad WHERE UsuarioPK = @UsuarioPK""";
            await _db.ExecuteAsync(sql, new {Rut=usuario.Rut, Nombre=usuario.Nombre, Edad=usuario.Edad, UsuarioPK=UsuarioPK });
        }

        public async Task delete(int UsuarioPK)
        {
            var sql = """
            DELETE FROM usuario 
            WHERE UsuarioPK = @UsuarioPK
            """;
            await _db.ExecuteAsync(sql, new { UsuarioPK });
        }

        public async Task<IEnumerable<Usuario>> getAll()
        {
            var sql = """
            SELECT * FROM usuario
            """;
            return await _db.QueryAsync<Usuario>(sql);
        }

        public async Task<Usuario> getByPK(int UsuarioPK)
        {
            var sql = """
            SELECT * FROM usuario 
            WHERE UsuarioPK = @UsuarioPK
            """;
            var user =await _db.QuerySingleOrDefaultAsync<Usuario>(sql, new { UsuarioPK });

            if (user == null)
            {
                throw new KeyNotFoundException("Usuario no existe");
            }
                

            return user;
        }
    }

}
