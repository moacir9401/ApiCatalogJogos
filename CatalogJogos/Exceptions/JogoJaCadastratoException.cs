using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogJogos.Exceptions
{
    public class JogoJaCadastratoException : Exception
    {
        public JogoJaCadastratoException() : base("Esse jogo já está cadastrado") { }
    }
}
