

namespace FinanceApi.Middleware
{
  public class RequestLoggingMiddleware
  {
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      var request = context.Request;

      // Log the request details
      Console.WriteLine($"{request.Method} {request.Path} ");

      // Log the request body if it exists
      if (request.ContentLength != null && request.ContentLength > 0)
      {
        var bodyStream = new MemoryStream();
        await request.Body.CopyToAsync(bodyStream);
        bodyStream.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(bodyStream).ReadToEndAsync();
        Console.WriteLine($"Body: {bodyText}");
        request.Body.Seek(0, SeekOrigin.Begin);
      }

      await _next(context);
    }
  }
}
