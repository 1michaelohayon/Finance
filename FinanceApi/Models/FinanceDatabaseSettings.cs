namespace FinanceApi.Models;

public class FinanceDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;

  public string DatabaseName { get; set; } = null!;

  public string UsersCollectionName { get; set; } = null!;

  public string LiabilitiesCollectionName { get; set; } = null!;
}