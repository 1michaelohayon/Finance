using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceApi.Models;

public class User
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string Username { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string FullName { get; set; } = null!;
  public List<String> CustomTags { get; set; } = new List<String>();
}