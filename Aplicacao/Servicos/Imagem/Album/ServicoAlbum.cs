using Aplicacao.Servicos.Alimentacao;
using Nest;
using System;

namespace Aplicacao.Servicos.Imagem.Album
{
    public class ServicoAlbum : IServicoAlbum
    {
        private readonly ElasticsearchSettings _elasticsearchSettings;

        public ServicoAlbum(
            ElasticsearchSettings elasticsearchSettings
        )
        {
            _elasticsearchSettings = elasticsearchSettings;
        }

        public ISearchResponse<JsonFormatado> RetornarAlbumsPorUsuario(int id)
        {
            Uri EsInstance = new Uri(_elasticsearchSettings.uri);
            ConnectionSettings EsConfiguration = new ConnectionSettings(EsInstance).DefaultIndex(_elasticsearchSettings.defaultIndex).DisableDirectStreaming();
            ElasticClient client = new ElasticClient(EsConfiguration);

            var searchResponse = client.Search<JsonFormatado>(s => s
                .Query(q => q
                    .Match(b => b
                        .Field(f => f.UserId)
                        .Query(id.ToString())
                    )
                )
            );

            return searchResponse;
        }
    }
}
