namespace TheKeep.Models;

public class Keep
{
  public int Id { get; set; }
  public string creatorId { get; set; }
  public string name { get; set; }
  public string description { get; set; }
  public string img { get; set; }
  public int views { get; set; }
  public int kept { get; set; }
  public Profile Creator { get; set; }

}