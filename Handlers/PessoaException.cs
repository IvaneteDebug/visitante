namespace Dev.visitante.Handlers
{
    public class PessoaException : Exception
    {
        public int StatusCode { get; }
        public PessoaException(string message, int statusCode = 500) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}