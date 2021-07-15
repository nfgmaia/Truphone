using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Truphone.Api.Filters
{
    public class ExceptionLoggerFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionLoggerFilter> logger;

        public ExceptionLoggerFilter(ILogger<ExceptionLoggerFilter> logger)
        {
            this.logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);
            await base.OnExceptionAsync(context);
        }
    }
}
