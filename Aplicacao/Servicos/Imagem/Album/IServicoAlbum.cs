using Aplicacao.Servicos.Alimentacao;
using System.Collections.Generic;

namespace Aplicacao.Servicos.Imagem.Album
{
    public interface IServicoAlbum
    {
        IEnumerable<JsonFormatado> RetornarAlbumsPorUsuario(int id);
    }
}
