public class Vault
{
  public int Id { get; set; }
  public string creatorId { get; set; }
  public string name { get; set; }
  public string description { get; set; }
  public string img { get; set; }
  public bool isPrivate { get; set; }
  public Profile Creator { get; set; }
}