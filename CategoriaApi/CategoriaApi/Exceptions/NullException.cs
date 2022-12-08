using System;

namespace CategoriaApi.Exceptions
{
    public class NullException: Exception
    {
        private const string mensagemPadrao = "o campo não encontrado";

        public NullException() : base(mensagemPadrao)
        {

        }
        public NullException(string mensagem): base(mensagem)
        {

        }

        public NullException(Exception innerException): base(mensagemPadrao, innerException) { }
    }
}
