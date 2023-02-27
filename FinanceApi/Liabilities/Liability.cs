using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinanceApi.Models;

public class Liability
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string? User { get; set; } = null!;
  [BsonElement("Name")]
  public string Name { get; set; } = null!;
  public decimal Amount { get; set; }
  public bool Recurring { get; set; } = false;
  public bool Active { get; set; } = true;
  public List<string> Tags { get; set; } = new List<string>();
  public decimal InterestRate { get; set; }
  public decimal MinimumPayment { get; set; }
  private DateTime PaymentStartDate { get; set; }
  private DateTime PaymentEndDate { get; set; }
  private DateTime PaymentDueDate { get; set; }
}