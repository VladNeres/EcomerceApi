using System;

namespace CategoriaApi.Exceptions
{
    public class InativeObjectException: Exception
    {
        private const string menssagemPadrao = "Objeto referenciado está inativo";

        public InativeObjectException():base(menssagemPadrao){}

        public InativeObjectException(string mensagem):base(mensagem)
        {

        }
        public InativeObjectException(Exception innerException):base(menssagemPadrao, innerException)
        {

        }
    }
}
