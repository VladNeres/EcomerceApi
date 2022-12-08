using System;

namespace CategoriaApi.Exceptions
{
    public class MinCharacterException: Exception
    {
        private const string mensagemPadrao = "Minimo de caracteres não atingido";
        public MinCharacterException(): base(mensagemPadrao)
        {

        }

        public MinCharacterException(string mensagem): base(mensagem)
        {

        }

        public MinCharacterException(Exception innerException) : base(mensagemPadrao, innerException)
        {

        }
    }
}
