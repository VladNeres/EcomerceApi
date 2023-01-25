using System;

namespace UsuariosApi.Exceptions
{
    public class MinCatacterException: Exception
    {
        public const string DefaultMessage = "Minimo de caracteres não atingidos";

        public MinCatacterException(): base(DefaultMessage)
        {

        }

        public MinCatacterException(string message): base(message)
        {

        }

        public MinCatacterException(Exception innerException): base(DefaultMessage, innerException)
        {

        }
    }
}
