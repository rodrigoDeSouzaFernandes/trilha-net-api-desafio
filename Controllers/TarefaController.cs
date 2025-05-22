using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;
using trilhaNetApiDesafio.Exceptions;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                Tarefa tarefa = _tarefaService.ObterPorId(id);
                return Ok(tarefa);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(
                    500,
                    new { Error = "Erro interno no servidor. Tente novamente mais tarde." }
                );
            }
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            List<Tarefa> tarefas = _tarefaService.ObterTodos();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            try
            {
                List<Tarefa> tarefas = _tarefaService.ObterPorTitulo(titulo);
                return Ok(tarefas);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Erro interno" });
            }
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            try
            {
                var tarefa = _tarefaService.ObterPorData(data);
                return Ok(tarefa);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Erro interno" });
            }
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            try
            {
                List<Tarefa> tarefas = _tarefaService.ObterPorStatus(status);
                return Ok(tarefas);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Erro interno" });
            }
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            try
            {
                _tarefaService.Criar(tarefa);

                return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(
                    500,
                    new { Error = "Erro interno no servidor. Tente novamente mais tarde." }
                );
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            try
            {
                _tarefaService.Atualizar(id, tarefa);

                return Ok(new { Message = "Tarefa atualizada com sucesso!" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(
                    500,
                    new { Error = "Erro interno no servidor. Tente novamente mais tarde." }
                );
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _tarefaService.Deletar(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(
                    500,
                    new { Error = "Erro interno no servidor. Tente novamente mais tarde." }
                );
            }
        }
    }
}
