using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FinanceApi.Tests
{
  [TestFixture]
  public class LiabilitiesTests
  {

    private static readonly HttpClient httpClient = new HttpClient();

    private string baseUrl = Environment.GetEnvironmentVariable("BASE_URL");
    private Auth0Singleton auth0 = new Auth0Singleton(httpClient);

    private string? createdLiabilityId;
    private string? createdLiabilityUser;
    private string? token;





    [SetUp]
    public void Setup()
    {
      if (this.token == null)
      {
        this.token = auth0.GetToken;
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      }

    }

    [Test, Order(1)]
    public void PostLiability()
    {

      // use token to post liability  
      var liability = new
      {
        name = "Test Liability",
        amount = 1000,
      };
      var jsonLiability = JsonConvert.SerializeObject(liability);
      var content = new StringContent(jsonLiability);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      var postResponse = httpClient.PostAsync($"{baseUrl}/api/liabilities", content).Result;
      //expect 201
      Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);
      //expect content in the request is the same as the response
      var jsonPostResponse = postResponse.Content.ReadAsStringAsync().Result;
      dynamic postResponseObject = JsonConvert.DeserializeObject(jsonPostResponse);
      Assert.AreEqual(liability.name, (string)postResponseObject.name);
      Assert.AreEqual(liability.amount, (int)postResponseObject.amount);

      //save the id of the created liability as setup for the next tests
      this.createdLiabilityId = (string)postResponseObject.id;
      this.createdLiabilityUser = (string)postResponseObject.user;

    }

    [Test, Order(2)]
    public void GetLiability()
    {


      // use token to get liability
      if (createdLiabilityId == null)
      {
        Assert.Fail("No liability created");
      }
      var liabilityResponse = httpClient.GetAsync($"{baseUrl}/api/liabilities/{createdLiabilityId}").Result;
      //expect 200
      Assert.AreEqual(HttpStatusCode.OK, liabilityResponse.StatusCode);
      //checks name, id and amount match
      var jsonLiabilityResponse = liabilityResponse.Content.ReadAsStringAsync().Result;
      dynamic liabilityResponseObject = JsonConvert.DeserializeObject(jsonLiabilityResponse);
      Assert.AreEqual("Test Liability", (string)liabilityResponseObject.name);
      Assert.AreEqual(1000, (int)liabilityResponseObject.amount);

    }

    [Test, Order(3)]
    public void PutLiability()
    {

      // use token to put liability
      if (createdLiabilityId == null)
      {
        Assert.Fail("No liability created");
      }
      var liability = new
      {
        id = createdLiabilityId,
        name = "REPLACED",
        amount = 5555,
        user = createdLiabilityUser
      };
      var jsonLiability = JsonConvert.SerializeObject(liability);
      var content = new StringContent(jsonLiability);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      var putResponse = httpClient.PutAsync($"{baseUrl}/api/liabilities/{createdLiabilityId}", content).Result;
      //expect 200
      Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode);

    }


    [Test, Order(4)]
    public void DeleteLiability()
    {

      // use token to delete liability
      if (createdLiabilityId == null)
      {
        Assert.Fail("No liability created");
      }
      var deleteResponse = httpClient.DeleteAsync($"{baseUrl}/api/liabilities/{createdLiabilityId}").Result;
      //expect 204
      Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }


    //post more liabilities
    [Test, Order(5)]
    public void postLiabilities()
    {

      // use token to post liability  
      var liability = new
      {
        name = "Test Liability 2",
        amount = 2000,
      };
      var jsonLiability = JsonConvert.SerializeObject(liability);
      var content = new StringContent(jsonLiability);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      var postResponse = httpClient.PostAsync($"{baseUrl}/api/liabilities", content).Result;
      //expect 201
      Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);
      //expect content in the request is the same as the response
      var jsonPostResponse = postResponse.Content.ReadAsStringAsync().Result;
      dynamic postResponseObject = JsonConvert.DeserializeObject(jsonPostResponse);
      Assert.AreEqual(liability.name, (string)postResponseObject.name);
      Assert.AreEqual(liability.amount, (int)postResponseObject.amount);

      //post another liability
      liability = new
      {
        name = "Test Liability 3",
        amount = 3000,
      };
      jsonLiability = JsonConvert.SerializeObject(liability);
      content = new StringContent(jsonLiability);
      content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      postResponse = httpClient.PostAsync($"{baseUrl}/api/liabilities", content).Result;
      //expect 201
      Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode);
      //expect content in the request is the same as the response
      jsonPostResponse = postResponse.Content.ReadAsStringAsync().Result;
      postResponseObject = JsonConvert.DeserializeObject(jsonPostResponse);
      Assert.AreEqual(liability.name, (string)postResponseObject.name);
      Assert.AreEqual(liability.amount, (int)postResponseObject.amount);

    }




    [Test, Order(6)]
    public void GetLiabilities()
    {

      // use token to get liabilities
      var liabilitiesResponse = httpClient.GetAsync($"{baseUrl}/api/liabilities").Result;
      //expect 200
      Assert.AreEqual(HttpStatusCode.OK, liabilitiesResponse.StatusCode);
      //expect to find 2 liabilities created in previous tests
      var jsonLiabilitiesResponse = liabilitiesResponse.Content.ReadAsStringAsync().Result;
      dynamic liabilitiesResponseObject = JsonConvert.DeserializeObject(jsonLiabilitiesResponse);

      Assert.AreEqual(2, liabilitiesResponseObject.Count);

      //expect not to find the liability deleted in the previous test
      foreach (var liability in liabilitiesResponseObject)
      {
        if (liability.id == createdLiabilityId)
        {
          Assert.Fail("Liability not deleted");
        }
      }
    }

    [Test, Order(7)]
    public void DeleteLiabilities()
    {

      // use token to delete liabilities
      var deleteResponse = httpClient.DeleteAsync($"{baseUrl}/api/liabilities/deleteall").Result;
      //expect 204
      Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Test, Order(8)]
    public void GetLiabilitiesAfterDelete()
    {

      // use token to get liabilities
      var liabilitiesResponse = httpClient.GetAsync($"{baseUrl}/api/liabilities").Result;
      //expect 200
      Assert.AreEqual(HttpStatusCode.OK, liabilitiesResponse.StatusCode);
      //expect to find 0 liabilities
      var jsonLiabilitiesResponse = liabilitiesResponse.Content.ReadAsStringAsync().Result;
      dynamic liabilitiesResponseObject = JsonConvert.DeserializeObject(jsonLiabilitiesResponse);

      Assert.AreEqual(0, liabilitiesResponseObject.Count);
    }




    [Test, Order(9)]
    public void CantUseLiabilitiesWithoutToken()
    {
      //remove header
      httpClient.DefaultRequestHeaders.Authorization = null;

      //expect 401
      var liabilitiesResponse = httpClient.GetAsync($"{baseUrl}/api/liabilities").Result;
      Assert.AreEqual(HttpStatusCode.Unauthorized, liabilitiesResponse.StatusCode);

      //expect 401
      var liabilityResponse = httpClient.GetAsync($"{baseUrl}/api/liabilities/123").Result;
      Assert.AreEqual(HttpStatusCode.Unauthorized, liabilityResponse.StatusCode);

      //expect 401
      var postResponse = httpClient.PostAsync($"{baseUrl}/api/liabilities", null).Result;
      Assert.AreEqual(HttpStatusCode.Unauthorized, postResponse.StatusCode);

      //expect 401
      var putResponse = httpClient.PutAsync($"{baseUrl}/api/liabilities/123", null).Result;
      Assert.AreEqual(HttpStatusCode.Unauthorized, putResponse.StatusCode);

      //expect 401
      var deleteResponse = httpClient.DeleteAsync($"{baseUrl}/api/liabilities/123").Result;
      Assert.AreEqual(HttpStatusCode.Unauthorized, deleteResponse.StatusCode);

      //expect 401
      var deelteall = httpClient.DeleteAsync($"{baseUrl}/api/liabilities/deleteall").Result;
      Assert.AreEqual(HttpStatusCode.Unauthorized, deelteall.StatusCode);
    }


  }



}