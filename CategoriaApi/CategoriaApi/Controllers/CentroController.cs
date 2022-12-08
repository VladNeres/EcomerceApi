using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentroController: ControllerBase
    {

        private DatabaseContext _context;
        private IMapper _mapper;
        private CentroDeDistribuicaoService _service;
        public CentroController(DatabaseContext context, IMapper mapper, CentroDeDistribuicaoService service)
        {
            _service = service;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCentro([FromBody] CreateCentroDto centroDto )
        {
            try
            {
                ReadCentroDto readCentro = await _service.AddCentroDeDistribuicao(centroDto);
                return CreatedAtAction(nameof(GetCentroPorId), new { id = readCentro.Id }, readCentro);
            }
            catch (AlreadyExistException e)
            {
                return BadRequest(e.Message);
            }
            catch (NullException)
            {
                return BadRequest("Falha na requisição do endereço");
            }
            catch (MinCharacterException)
            {
                return BadRequest("Minimo de 3 carcteres  necessario não atingido ");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCentro(int id, [FromBody] UpdateCentroDto updateCentro)
        {
            try
            {
                var centro = _service.AtualizarCentroService(id, updateCentro);
                if (centro.IsFailed) return NotFound();
                return NoContent();

            }
            catch (NullException)
            {
                return NotFound("Id não encontrado");
            }
            catch(InativeObjectException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCentro(int id)
        {
            Result result = _service.ExcluirCentro(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
       public IActionResult GetCentroPorId(int id)
       {
            ReadCentroDto readCentro = _service.RecuperarCentroPorId(id);
            if (readCentro != null) return Ok(readCentro);
            return NotFound("Não encontrado");
       }

        [HttpGet]
        public List<CentroDeDistribuicao> GetCentroDeDistribuicao([FromBody] CentroPesquisa pesquisa)
        {
            return _service.GetCentroDeDistribuicao(pesquisa);
        }
    }
}
