using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Imagem
{
    public class Foto
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }

        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }
    }
}
