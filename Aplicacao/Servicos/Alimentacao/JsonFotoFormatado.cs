using Nest;

namespace Aplicacao.Servicos.Alimentacao
{
    public class JsonFotoFormatado
    {
        [Text(Name = "Id")]
        public int Id { get; set; }
        [Text(Name = "Title")]
        public string Title { get; set; }
        [Text(Name = "Url")]
        public string Url { get; set; }
        [Text(Name = "ThumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }
}
