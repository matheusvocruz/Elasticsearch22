using Aplicacao.Servicos.Alimentacao;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<JsonFormatado> RetornarAlbumsPorUsuario(int id)
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

            return searchResponse.HitsMetadata.Hits.Select(x => x.Source).ToList().Select(x => new JsonFormatado
            {
                Id = x.Id,
                AlbumTitle = x.AlbumTitle,
                UserId = x.UserId,
                Fotos = x.Fotos
            });
        }
    }
}
