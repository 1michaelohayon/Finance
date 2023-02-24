using FinanceApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinanceApi.Services;

public class LiabilitiesService
{
  private readonly IMongoCollection<Liability> _liabilitiesCollection;
  private readonly IMongoCollection<User> _usersCollection;

  public LiabilitiesService(
      IOptions<FinanceDatabaseSettings> financeDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      financeDatabaseSettings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(
      financeDatabaseSettings.Value.DatabaseName);

    _liabilitiesCollection = mongoDatabase.GetCollection<Liability>(
     financeDatabaseSettings.Value.LiabilitiesCollectionName);

    _usersCollection = mongoDatabase.GetCollection<User>(
      financeDatabaseSettings.Value.UsersCollectionName);
  }


  public async Task<List<Liability>> GetLiabilitiesAsync() =>
    await _liabilitiesCollection.Find(_ => true).ToListAsync();



  public async Task<Liability> GetLiabilityAsync(string id) =>
    await _liabilitiesCollection.Find(liability => liability.Id == id).FirstOrDefaultAsync();



  public async Task<List<Liability>> GetUserLiabilitiesAsync(string id) =>
    await _liabilitiesCollection.Find(liability => liability.User == id).ToListAsync();





  public async Task<Liability> CreateLiabilityAsync(Liability liability)
  {

    string id = liability.User.ToString();
    User user = await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
    if (user == null)
    {
      throw new Exception("User not found");
    }

    await _liabilitiesCollection.InsertOneAsync(liability);

    return liability;
  }





  public async Task UpdateLiabilityAsync(string id, Liability liability) =>
    await _liabilitiesCollection.ReplaceOneAsync(liability => liability.Id == id, liability);



  public async Task RemoveLiabilityAsync(string id) =>
    await _liabilitiesCollection.DeleteOneAsync(liability => liability.Id == id);


}