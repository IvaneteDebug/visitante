using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dev.visitante.Handlers
{
    public class PessoaHandlerException : IPessoaHandlerException
    {
        public Task HandleExceptionAsync(ExceptionContext context)
        {
            var statusCode = 500;

            if (context.Exception is PessoaException pessoaException)
            {
                statusCode = pessoaException.StatusCode;
            }

            context.Result = new ObjectResult(new { error = context.Exception.Message })
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }
    }
}
