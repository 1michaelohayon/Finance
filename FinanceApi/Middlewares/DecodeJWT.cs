using System.IdentityModel.Tokens.Jwt;

namespace FinanceApi.Middleware
{
  public class DecodedJWT
  {
    private readonly RequestDelegate _next;

    public DecodedJWT(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      if (context.Request.Path.StartsWithSegments("/health"))
      {
        await _next(context);
        return;
      }
      // Get the JWT token from the authorization header, if it exists, otherwise it returns 401
      string? authorizationHeader = context.Request.Headers["Authorization"]!;
      if (authorizationHeader == null)
      {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Authorization header missing");
        return;
      }

      string? token = authorizationHeader?.Replace("Bearer ", "");


      // If the token is null, it returns 401
      if (token == null)
      {
        context.Response.StatusCode = 401;
        return;
      }

      // Decode the JWT token and then it adds the claims to the context
      try
      {
        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadJwtToken(token);
        context.Items["UserClaims"] = decodedToken.Claims.ToList();
        Console.WriteLine("passed");
        Console.WriteLine($"Decoded token: {decodedToken.Claims} ...");

        //console write line all the claims
        foreach (var claim in decodedToken.Claims)
        {
          Console.WriteLine($"Claim: {claim.Type} - {claim.Value}");
        }


        await _next(context);
      }
      catch (Exception ex)
      {
        Console.WriteLine("DecodeJWT ERROR: " + ex);
        context.Items["UserClaims"] = null;
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Authentication failed");
      }
    }
  }
}
