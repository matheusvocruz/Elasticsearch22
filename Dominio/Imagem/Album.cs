using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Imagem
{
    public class Album
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }

        [InverseProperty("Album")]
        public ICollection<Foto> Fotos { get; set; } = new HashSet<Foto>();
        
    }
}
