namespace TheKeep.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _vaultKeepsRepo;
  private readonly VaultsService _vs;


  public VaultKeepsService(VaultKeepsRepository repo, VaultsService @vs)
  {
    _vaultKeepsRepo = repo;
    _vs = @vs;
  }



  internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, string userId)
  {
    Vault vault = _vs.GetByVaultId(newVaultKeep.vaultId);
    if (vault.creatorId != userId)
    {
      throw new Exception("You're not a very nice fella aren't you?");
    }


    return _vaultKeepsRepo.CreateVaultKeep(newVaultKeep);
  }




  public void DeleteVaultKeep(int id, string userId)
  {
    var vaultKeep = GetByVaultKeepId(id);
    if (vaultKeep.creatorId != userId)
    {
      throw new Exception("Insufficient Permission to Delete!");
    }
    _vaultKeepsRepo.DeleteVaultKeep(id);
  }


  internal VaultKeep GetByVaultKeepId(int id)
  {
    var foundVaultKeep = _vaultKeepsRepo.GetVaultKeepById(id);
    if (foundVaultKeep == null)
    {
      throw new Exception("This vaultKeep does not exist");
    }
    return foundVaultKeep;
  }




}
