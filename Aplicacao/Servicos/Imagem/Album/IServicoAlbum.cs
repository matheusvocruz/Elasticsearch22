using Aplicacao.Servicos.Alimentacao;
using Nest;
using System.Collections.Generic;

namespace Aplicacao.Servicos.Imagem.Album
{
    public interface IServicoAlbum
    {
        ISearchResponse<JsonFormatado> RetornarAlbumsPorUsuario(int id);
    }
}
