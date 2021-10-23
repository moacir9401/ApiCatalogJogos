using CatalogJogos.InputModel;
using CatalogJogos.Services;
using CatalogJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogJogos.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService service)
        {
            _jogoService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if (!jogos.Any())
            {
                return NoContent();
            }

            return Ok(jogos);
        }

        [HttpGet("{idJogo: guild}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogos = await _jogoService.Obter(idJogo);

            if (jogos == null)
            {
                return NoContent();
            }

            return Ok(jogos);
        }

        [HttpPost()]
        public async Task<ActionResult<JogoViewModel>> Inserir([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);

                return Ok(jogo);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo: guild}")]
        public async Task<ActionResult> AtualizarJogo([FromQuery] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {

            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);

                return Ok();
            }
            catch (Exception)
            {
                return UnprocessableEntity("Não existe esse jogo");
            }
        }

        [HttpPatch("{idJogo: guild}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromQuery] Guid idJogo, [FromQuery] double preco)
        {
            try
            {
                try
                {
                    await _jogoService.Atualizar(idJogo, preco);

                    return Ok();
                }
                catch (Exception)
                {
                    return UnprocessableEntity("Não existe esse jogo");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{idJogo: guild}")]
        public async Task<ActionResult> ApagarJogo([FromQuery] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (Exception)
            {

                return UnprocessableEntity("Não existe esse jogo");
            }
        }

    }
}
