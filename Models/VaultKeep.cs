using TheKeep.Interfaces;

namespace TheKeep.Models;

public class VaultKeep : IRepoItem<int>
{
  public int Id { get; set; }
  public string creatorId { get; set; }
  public int vaultId { get; set; }
  public int keepId { get; set; }
  public Profile Creator { get; set; }

}

public class VaultKeepViewModel : Keep
{
  public int vaultKeepId { get; set; }
}