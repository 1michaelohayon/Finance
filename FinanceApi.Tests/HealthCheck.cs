using System.Net;


namespace HealthCheck
{
  [TestFixture]
  public class HealthCheckTest
  {
    [Test]
    public async Task HealthCheck()
    {
      var client = new HttpClient();
      var response = await client.GetAsync("http://localhost:5227/health");
      var content = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
      Assert.That(content, Is.EqualTo("ok"));
    }
  }
}