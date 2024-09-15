using System.Net;

namespace RealEstate.PL.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var htmlResponse = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Error - Something Went Wrong</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background: linear-gradient(135deg, #f8f9fa, #e9ecef);
                        color: #333;
                        display: flex;
                        justify-content: center;
                        align-items: center;
                        height: 100vh;
                        margin: 0;
                        text-align: center;
                    }
                    .container {
                        background: #fff;
                        padding: 40px;
                        border-radius: 8px;
                        box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
                        max-width: 600px;
                        width: 100%;
                    }
                    h1 {
                        font-size: 2.5rem;
                        color: #dc3545;
                        margin-bottom: 20px;
                    }
                    p {
                        font-size: 1.2rem;
                        margin-bottom: 30px;
                    }
                    a {
                        color: #007bff;
                        text-decoration: none;
                        font-weight: bold;
                        font-size: 1.1rem;
                    }
                    a:hover {
                        text-decoration: underline;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Oops! Something went wrong.</h1>
                    <p>We're sorry, but something went wrong on our end. Please try again later.</p>
                    <a href='/'>Back to Home</a>
                </div>
            </body>
            </html>";

            return context.Response.WriteAsync(htmlResponse);
        }
    }
}