using System.Net;
using NUnit.Framework;

namespace FinanceApi.Tests
{

  [TestFixture]
  public class HealthCheckTest
  {
    private string baseUrl = "http://localhost:5227";

    //dummy test
    [Test]
    public void DummyTest()
    {
      Assert.AreEqual(1, 1);
    }

    //Test that the server is working
    [Test]
    public async Task HealthCheck()
    {
      var client = new HttpClient();
      var response = await client.GetAsync($"{baseUrl}/health");
      var content = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.That(content, Is.EqualTo("ok"));
    }

    //Test that mongodb is working
    [Test]
    public async Task MongoHealthCheck()
    {
      var client = new HttpClient();
      var response = await client.GetAsync($"{baseUrl}/health/mongo");
      var content = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.That(content, Is.EqualTo("ok"));
    }

  }
}