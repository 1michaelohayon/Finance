using FinanceApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinanceApi.Services;

public class LiabilitiesService
{
  private readonly IMongoCollection<Liability> _liabilitiesCollection;

  public LiabilitiesService(
      IOptions<FinanceDatabaseSettings> financeDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      financeDatabaseSettings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(
      financeDatabaseSettings.Value.DatabaseName);

    _liabilitiesCollection = mongoDatabase.GetCollection<Liability>(
     financeDatabaseSettings.Value.LiabilitiesCollectionName);


  }


  public async Task<List<Liability>> GetLiabilities() =>
    await _liabilitiesCollection.Find(_ => true).ToListAsync();



  public async Task<Liability> GetLiability(string id) =>
    await _liabilitiesCollection.Find(liability => liability.Id == id).FirstOrDefaultAsync();



  public async Task<List<Liability>> GetUserLiabilities(string id) =>
    await _liabilitiesCollection.Find(liability => liability.User.Id == id).ToListAsync();





  public async Task<Liability> CreateLiability(Liability liability)
  {

    if (liability.User == null)
    {
      throw new Exception("User not found");
    }

    await _liabilitiesCollection.InsertOneAsync(liability);

    return liability;
  }





  public async Task UpdateLiability(string id, Liability liability) =>
    await _liabilitiesCollection.ReplaceOneAsync(liability => liability.Id == id, liability);



  public async Task RemoveLiability(string id) =>
    await _liabilitiesCollection.DeleteOneAsync(liability => liability.Id == id);


}