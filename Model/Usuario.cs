using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace clase08.Model
{
    public class Usuario
    {
        [Key]
        [JsonIgnore]
        public int UsuarioPK { get; set; }
        public string ? Rut {  get; set; }
        public string ? Nombre { get; set; }        
        public Int16 ? Edad {  get; set; }
        //public Gustos[] GustosUsuario { get; set; }
    }

    public class Gustos
    {
        public int IdTipoMusica { get; set; }
        public string ? TipoMusica { get; set; }
    }
}
