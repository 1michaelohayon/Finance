using System.Net;
using System;

namespace FinanceApi.Tests
{
  [TestFixture]
  public class UsersTests
  {

    [Test]
    public async Task GetUsers()
    {
      var client = new HttpClient();
      var response = await client.GetAsync("http://localhost:5227/api/users");
      var content = await response.Content.ReadAsStringAsync();

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  }
}
