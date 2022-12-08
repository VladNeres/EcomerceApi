using System;

namespace CategoriaApi.Exceptions
{
    public class AlreadyExistException: Exception
    {
        private const string menssagemPadrao = "ja existe um objeto com esse nome";   
        public AlreadyExistException():base(menssagemPadrao)
        {

        }

        public AlreadyExistException(string mensagem) : base(mensagem) 
        {

        }

        public AlreadyExistException(Exception innerException): base(menssagemPadrao,innerException)
        {

        }
        

    }
}
