using System;

namespace UsuariosApi.Exceptions
{
    public class NullException : Exception
    {
        private const string DefaultMessage = "O objeto referenciado é  nullo ";

        public NullException():base(DefaultMessage)
        {

        }

        public NullException(string mensagem): base(mensagem)
        {

        }

        public NullException(Exception innerException): base(DefaultMessage, innerException)
        {

        }
    }
}
