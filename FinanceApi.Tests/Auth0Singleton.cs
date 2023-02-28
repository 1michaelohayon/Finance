using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace FinanceApi.Tests
{

  public class Auth0Singleton
  {
    //get audience from auth0 in FinanceApi.models

    private HttpClient httpClient;

    private string? auth0Url = Environment.GetEnvironmentVariable("AUTH0_URL");
    private string? audience = Environment.GetEnvironmentVariable("AUTH0_AUDIENCE");
    private string? clientId = Environment.GetEnvironmentVariable("AUTH0_CLIENT_ID");
    private string? clientSecret = Environment.GetEnvironmentVariable("AUTH0_CLIENT_SECRET");

    public Auth0Singleton(HttpClient httpClient)
    {
      if (auth0Url == null)
      {
        throw new Exception("AUTH0_URL is not set");
      }
      else if (audience == null)
      {
        throw new Exception("AUTH0_AUDIENCE is not set");
      }
      else if (clientId == null)
      {
        throw new Exception("AUTH0_CLIENT_ID is not set");
      }
      else if (clientSecret == null)
      {
        throw new Exception("AUTH0_CLIENT_SECRET is not set");
      }


      this.httpClient = httpClient;
    }

    private string? _token;
    public string? GetToken
    {
      get
      {
        if (this._token == null)
        {
          createToken();
        }
        return this._token;
      }
    }




    private void createToken()
    {

      var requestData = new
      {
        client_id = clientId,
        client_secret = clientSecret,
        audience = audience,
        grant_type = "client_credentials"
      };

      var jsonRequest = JsonConvert.SerializeObject(requestData);
      var content = new StringContent(jsonRequest);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      var response = httpClient.PostAsync(auth0Url, content).Result;
      var jsonResponse = response.Content.ReadAsStringAsync().Result;
      dynamic tokenObject = JsonConvert.DeserializeObject(jsonResponse);

      this._token = (string)tokenObject.access_token;
    }
  }


}
