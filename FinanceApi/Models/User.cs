using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceApi.Models;

public class User
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }

  [BsonElement("Username")]
  public string Username { get; set; } = null!;

  [BsonElement("Email")]
  public string Email { get; set; } = null!;

  [BsonElement("FullName")]
  public string FullName { get; set; } = null!;

  public List<String> CustomTags { get; set; } = new List<String>();

  public List<Liability> Liabilities { get; set; } = new List<Liability>();


}