using Microsoft.AspNetCore.Mvc.Filters;

namespace Dev.visitante.Handlers
{
    public interface IPessoaHandlerException
    {
        Task HandleExceptionAsync(ExceptionContext context);
    }
}
