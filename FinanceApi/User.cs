namespace FinanceApi.Models
{
  public class User
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
    public string Username { get; set; }
    public Boolean EmailVerified { get; set; }
  }
}