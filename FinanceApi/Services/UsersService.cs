using FinanceApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FinanceApi.Services;


public class UsersService
{
  private readonly IMongoCollection<User> _usersCollection;

  public UsersService(
      IOptions<FinanceDatabaseSettings> financeDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      financeDatabaseSettings.Value.ConnectionString);

    var mongoDatabase = mongoClient.GetDatabase(
      financeDatabaseSettings.Value.DatabaseName);

    _usersCollection = mongoDatabase.GetCollection<User>(
     financeDatabaseSettings.Value.UsersCollectionName);
  }
  public async Task<List<User>> GetUsersAsync() =>
  await _usersCollection.Find(_ => true).ToListAsync();

  public async Task<User> CreateUserAsync(User user)
  {
    await _usersCollection.InsertOneAsync(user);
    return user;
  }

  public async Task<User> GetUserAsync(string id) =>
    await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();




}