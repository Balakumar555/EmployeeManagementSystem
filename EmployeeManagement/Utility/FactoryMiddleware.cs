namespace EmployeeManagement.Utility
{
    
    public class FactoryMiddleware : IMiddleware
    {
        private readonly ILogger<FactoryMiddleware> _logger;

        public FactoryMiddleware(ILogger<FactoryMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _logger.LogInformation("Before Request");
            await next(context);
            _logger.LogInformation("After request");
        }
    }
}
