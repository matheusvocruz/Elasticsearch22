using Aplicacao.Servicos.Imagem.Album;
using Microsoft.AspNetCore.Mvc;

namespace FotosApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IServicoAlbum _servicoAlbum;

        public UserController(IServicoAlbum servicoAlbum)
        {
            _servicoAlbum = servicoAlbum;
        }

        [HttpGet("{id}/albums")]
        public IActionResult Index([FromRoute] int id)
        {
            return Ok(_servicoAlbum.RetornarAlbumsPorUsuario(id));
        }
    }
}
