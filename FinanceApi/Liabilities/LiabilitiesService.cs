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


  //get single liablity with mongodb id from the user's liabilities
  public async Task<Liability> GetLiability(string id) =>
    await _liabilitiesCollection.Find(liability => liability.Id == id).FirstOrDefaultAsync();


  //get user's liabilities
  public async Task<List<Liability>> GetLiabilities(string id) =>
    await _liabilitiesCollection.Find(liability => liability.User == id).ToListAsync();


  public async Task<Liability> CreateLiability(Liability liability)
  {
    if (liability.User == null)
    {
      throw new Exception("User not found");
    }
    await _liabilitiesCollection.InsertOneAsync(liability);
    return liability;
  }



  //updates liability
  public async Task<Liability> UpdateLiability(Liability liability, string id)
  {
    var updated = await _liabilitiesCollection.ReplaceOneAsync(liability => liability.Id == id, liability);

    if (updated.IsAcknowledged)
    {
      return liability;
    }
    else
    {
      throw new Exception("Liability not updated");
    }
  }



  // delete liability
  public async Task RemoveLiability(string id) =>
    await _liabilitiesCollection.DeleteOneAsync(liability => liability.Id == id);

  // removes all the liabilities of a user
  public async Task RemoveAllLiabilities(string id) =>
    await _liabilitiesCollection.DeleteManyAsync(liability => liability.User == id);

}