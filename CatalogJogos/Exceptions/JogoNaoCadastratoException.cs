using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogJogos.Exceptions
{
    public class JogoNaoCadastratoException : Exception
    {
        public JogoNaoCadastratoException() : base("Esse jogo não está cadastrado") { }
    }
}
