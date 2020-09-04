using Nest;
using System.Collections.Generic;

namespace Aplicacao.Servicos.Alimentacao
{
    public class JsonFormatado
    {
        [Text(Name = "AlbumId")]
        public int Id { get; set; }
        [Text(Name = "AlbumTitle")]
        public string AlbumTitle { get; set; }
        [Text(Name = "UserId")]
        public int UserId { get; set; }
        [Text(Name = "Fotos")]
        public List<JsonFotoFormatado> Fotos { get; set; }
    }
}
