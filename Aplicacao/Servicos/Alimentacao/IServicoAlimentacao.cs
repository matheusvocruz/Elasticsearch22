using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Servicos.Alimentacao
{
    public interface IServicoAlimentacao
    {
        IEnumerable<Dominio.Imagem.Foto> RetornarFotos();
        IEnumerable<Dominio.Imagem.Album> RetornarAlbums();
        List<JsonFormatado> RetornarAlbumsComFotos();
        List<JsonFormatado> RetornarAlbumsComFotosPorPessoa(int id);
        Task SubirInformacoesGerais();
        Task SubirInformacoesPorUsuario(int id);
    }
}
