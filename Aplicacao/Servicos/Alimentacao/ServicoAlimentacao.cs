using Dominio.Imagem;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aplicacao.Servicos.Alimentacao
{
    public class ServicoAlimentacao : IServicoAlimentacao
    {
        private readonly ElasticsearchSettings _elasticsearchSettings;

        public ServicoAlimentacao(
            ElasticsearchSettings elasticsearchSettings
        )
        {
            _elasticsearchSettings = elasticsearchSettings;
        }

        public IEnumerable<Dominio.Imagem.Foto> RetornarFotos()
        {
            using (var client = new HttpClient())
            {
                string baseUrl = "https://jsonplaceholder.typicode.com/photos";
                var response = client.GetAsync(baseUrl).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<IEnumerable<Dominio.Imagem.Foto>>(responseContent.ReadAsStringAsync().GetAwaiter().GetResult());
                }
            }

            return null;
        }

        public IEnumerable<Dominio.Imagem.Album> RetornarAlbums()
        {
            using (var client = new HttpClient())
            {
                string baseUrl = "https://jsonplaceholder.typicode.com/albums";
                var response = client.GetAsync(baseUrl).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    return JsonConvert.DeserializeObject<IEnumerable<Dominio.Imagem.Album>>(responseContent.ReadAsStringAsync().GetAwaiter().GetResult());
                }
            }

            return null;
        }

        public List<JsonFormatado> RetornarAlbumsComFotos()
        {
            var albums = this.RetornarAlbums();
            var fotos = this.RetornarFotos();
            var lista = new List<JsonFormatado>();

            foreach(Album album in albums)
            {
                lista.Add(new JsonFormatado
                {
                    Id = album.Id,
                    AlbumTitle = album.Title,
                    UserId = album.UserId,
                    Fotos = fotos.Where(x => x.AlbumId == album.Id).Select(x => new JsonFotoFormatado {
                        Id = x.Id,
                        ThumbnailUrl = x.ThumbnailUrl,
                        Title = x.Title,
                        Url = x.Url
                    }).ToList()
                });
            }

            return lista;
        }

        public List<JsonFormatado> RetornarAlbumsComFotosPorPessoa(int id)
        {
            var albums = this.RetornarAlbums().Where(x => x.UserId == id);
            var fotos = this.RetornarFotos();
            var lista = new List<JsonFormatado>();

            foreach (Album album in albums)
            {
                lista.Add(new JsonFormatado
                {
                    Id = album.Id,
                    AlbumTitle = album.Title,
                    UserId = album.UserId,
                    Fotos = fotos.Where(x => x.AlbumId == album.Id).Select(x => new JsonFotoFormatado
                    {
                        Id = x.Id,
                        ThumbnailUrl = x.ThumbnailUrl,
                        Title = x.Title,
                        Url = x.Url
                    }).ToList()
                });
            }

            return lista;
        }

        public async Task SubirInformacoesGerais()
        {
            var dados = RetornarAlbumsComFotos();

            foreach (JsonFormatado dado in dados)
            {
                IndexarDados(dado);
            }
        }

        public async Task SubirInformacoesPorUsuario(int id)
        {
            var dados  = RetornarAlbumsComFotosPorPessoa(id);

            foreach (JsonFormatado dado in dados)
            {
                IndexarDados(dado);
            }
        }

        private async Task IndexarDados(JsonFormatado dado)
        {
            Uri EsInstance = new Uri(_elasticsearchSettings.uri);
            ConnectionSettings EsConfiguration = new ConnectionSettings(EsInstance).DefaultIndex(_elasticsearchSettings.defaultIndex).DisableDirectStreaming();
            ElasticClient client = new ElasticClient(EsConfiguration);

            var index = client.Indices.Exists(dado.Id.ToString());

            if (index.Exists)
            {
                client.Indices.Delete(dado.Id.ToString());
            }

           client.IndexDocument(dado);
        }
    }
}