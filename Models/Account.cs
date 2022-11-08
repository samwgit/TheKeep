using TheKeep.Interfaces;


namespace TheKeep.Models;

public class Profile : IRepoItem<string>
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Picture { get; set; }
  public string coverImg { get; set; }
}


public class Account : Profile
{
  public new string Email { get; set; }
}