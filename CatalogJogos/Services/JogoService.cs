using CatalogJogos.Entities;
using CatalogJogos.Exceptions;
using CatalogJogos.InputModel;
using CatalogJogos.Repositories;
using CatalogJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _JogoRepository;

        public JogoService(IJogoRepository JogoRepository)
        {
            _JogoRepository = JogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _JogoRepository.Obter(pagina,quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid idJogo)
        {
            var jogo = await _JogoRepository.Obter(idJogo);

            if(jogo == null)
            {
                return null;
            }


            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _JogoRepository.Obter(jogo.Nome,jogo.Produtora);

            if (entidadeJogo.Any())
            {
                throw new JogoJaCadastratoException();
            }
            
            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };

            await _JogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };
        }

        public async Task Atualizar(Guid idJogo, JogoInputModel jogo)
        {
            var entidadeJogo = await _JogoRepository.Obter(idJogo);

            if (entidadeJogo == null)
            {
                throw new JogoNaoCadastratoException();
            }

            var jogoAlterar = new Jogo
            {
                Id = idJogo,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco

            };

            await _JogoRepository.Atualizar(jogoAlterar);
        }

        public async Task Atualizar(Guid idJogo, double preco)
        {
            var jogo = await _JogoRepository.Obter(idJogo);

            if (jogo == null)
            {
                throw new JogoNaoCadastratoException();
            }

            var jogoAlterar = new Jogo
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = preco

            };


            await _JogoRepository.Atualizar(jogoAlterar);
        }

        public async Task Remover(Guid idJogo)
        {
            var jogo = await _JogoRepository.Obter(idJogo);

            if (jogo == null)
            {
                throw new JogoNaoCadastratoException();
            }

            await _JogoRepository.Remover(idJogo);

        }

        public void Dispose()
        {
            _JogoRepository?.Dispose();
        }
    }
}
