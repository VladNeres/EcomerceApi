using System;

namespace UsuariosApi.Exceptions
{
    public class LoginOrPasswordException: Exception
    {
        private const string DefaultMessage= "Login ou senha incorreto";
        public LoginOrPasswordException():base(DefaultMessage)
        {

        }

        public LoginOrPasswordException(string message): base(message)
        {

        }

        public LoginOrPasswordException(Exception innerException):base (DefaultMessage, innerException)
        {

        }
    }
}
