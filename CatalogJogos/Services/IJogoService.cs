using CatalogJogos.InputModel;
using CatalogJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogJogos.Services
{
    public interface IJogoService
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid idJogo);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid idJogo, JogoInputModel jogo);
        Task Atualizar(Guid idJogo, double preco);
        Task Remover(Guid idJogo);
    }
}
