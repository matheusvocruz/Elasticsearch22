using Aplicacao.Servicos.Alimentacao;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace FotosApi.Controllers
{
    [Route("api/[controller]")]
    public class AlimentacaoController : Controller
    {
        private readonly IServicoAlimentacao _servicoAlimentacao;

        public AlimentacaoController(IServicoAlimentacao servicoAlimentacao)
        {
            _servicoAlimentacao = servicoAlimentacao;
        }

        [HttpGet("InformacoesGerais")]
        public IActionResult InformacoesGerais()
        {
            return Ok(_servicoAlimentacao.SubirInformacoesGerais());
        }

        [HttpGet("InformacoesPorUsuario/{id}")]
        public IActionResult InformacoesPorUsuario(int id)
        {
            return Ok(_servicoAlimentacao.SubirInformacoesPorUsuario(id));
        }
    }
}
