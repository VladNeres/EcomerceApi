using System;

namespace UsuariosApi.Exceptions
{
    public class AlreadyExistsException:Exception
    {
        public const string DefaultMessage = "O objeto já Existe";
        public AlreadyExistsException():base(DefaultMessage)
        {

        }

        public AlreadyExistsException(string message): base(message)
        {

        }

        public AlreadyExistsException(Exception innerException): base(DefaultMessage, innerException)
        {

        }
    }
}
