using CatalogJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {

        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            { Guid.Parse("3227a9f0-3a15-4ae1-a016-ca76cc7b043b"), new Jogo { Id = Guid.Parse("3227a9f0-3a15-4ae1-a016-ca76cc7b043b"), Nome = "Fifa 21", Produtora = "EA", Preco = 200 }},
            { Guid.Parse("409e5d25-89bf-44cc-988d-1768b63ae4e4"), new Jogo { Id = Guid.Parse("409e5d25-89bf-44cc-988d-1768b63ae4e4"), Nome = "Fifa 20", Produtora = "EA", Preco = 190 }},
            { Guid.Parse("cf3175f6-1c51-4311-9dbb-951727e36ca8"), new Jogo { Id = Guid.Parse("cf3175f6-1c51-4311-9dbb-951727e36ca8"), Nome = "Fifa 19", Produtora = "EA", Preco = 180 }},
            { Guid.Parse("aed70bc1-852d-4419-82d1-9205109acf75"), new Jogo { Id = Guid.Parse("aed70bc1-852d-4419-82d1-9205109acf75"), Nome = "Fifa 18", Produtora = "EA", Preco = 170 }},
            { Guid.Parse("3af0323a-2fed-4d65-844f-d78855e0e516"), new Jogo { Id = Guid.Parse("3af0323a-2fed-4d65-844f-d78855e0e516"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 80 }},
            { Guid.Parse("2dbba5a9-e5be-4541-9978-a3ce88ace72c"), new Jogo { Id = Guid.Parse("2dbba5a9-e5be-4541-9978-a3ce88ace72c"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 190 }},
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid idJogo)
        {
            if (!jogos.ContainsKey(idJogo))
            {
                return null;
            }

            return Task.FromResult(jogos[idJogo]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome == nome && jogo.Produtora == produtora).ToList());
        }

        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome == nome && jogo.Produtora == produtora)
                {
                    retorno.Add(jogo);
                }
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;


        }

        public Task Remover(Guid idJogo)
        {
            jogos.Remove(idJogo);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
           
        }

      

    }
}
