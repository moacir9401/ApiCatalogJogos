using CatalogJogos.Exceptions;
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

        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possivel retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registro por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <returns code="204">Caso não haja jogos</returns>
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

        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>
        [HttpGet("{idJogo:guid}")] 
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogos = await _jogoService.Obter(idJogo);

            if (jogos == null)
            {
                return NoContent();
            }

            return Ok(jogos);
        }

        /// <summary>
        /// Inserir um jogo
        /// </summary>
        /// <param name="jogoInputModel">Jogo a szer inserido</param>
        /// <response code="200">Insere o jogo</response>
        /// <response code="400">Algum campo não foi preenchido ou violou as validações</response>
        [HttpGet("{idJogo:guid}")]
        [HttpPost()]
        public async Task<ActionResult<JogoViewModel>> Inserir([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);

                return Ok(jogo);
            }
            catch (JogoJaCadastratoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }


        /// <summary>
        /// Atualiza o jogo buscando pelo id
        /// </summary>
        /// <param name="idJogo">Id do jogo a ser alterado</param>
        /// <param name="jogoInputModel">jogo a ser alterado</param>
        /// <response code="200">altera o jogo</response>
        /// <response code="400">Algum campo não foi preenchido ou violou as validações</response>
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromQuery] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {

            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);

                return Ok();
            }
            catch (JogoNaoCadastratoException ex)
            {
                return UnprocessableEntity("Não existe esse jogo");
            }
        }

        /// <summary>
        /// Atualiza o jogo buscando pelo id e alterando o preco
        /// </summary>
        /// <param name="idJogo">Id do jogo a ser alterado</param>
        /// <param name="preco">preço do jogo a ser alterado</param>
        /// <response code="200">altera o jogo</response>
        /// <response code="400">Algum campo não foi preenchido ou violou as validações</response>
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromQuery] Guid idJogo, [FromQuery] double preco)
        {
            try
            {
                try
                {
                    await _jogoService.Atualizar(idJogo, preco);

                    return Ok();
                }
                catch (JogoNaoCadastratoException ex)
                {
                    return UnprocessableEntity("Não existe esse jogo");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Apaga o jogo pelo id
        /// </summary>
        /// <param name="idJogo"></param>
        /// <response code="200">jogo deletado</response>
        /// <response code="404">Caso não haja jogo com este id</response>
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromQuery] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();
            }
            catch (JogoNaoCadastratoException ex)
            {

                return UnprocessableEntity("Não existe esse jogo");
            }
        }

    }
}
