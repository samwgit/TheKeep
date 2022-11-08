namespace TheKeep.Services;

public class VaultsService
{
  private readonly VaultsRepository _vaultsRepo;



  public VaultsService(VaultsRepository repo)
  {
    _vaultsRepo = repo;
  }

  internal Vault CreateVault(Vault newVault)
  {
    return _vaultsRepo.CreateVault(newVault);
  }








  public Vault UpdateVault(Vault vaultData, string userId)
  {
    Vault originalVault = GetByVaultId(vaultData.Id);
    if (originalVault.creatorId != userId)
    {
      throw new Exception("Insufficient Permission to Edit");
    }
    originalVault.name = vaultData.name ?? vaultData.name;
    originalVault.description = vaultData.description ?? vaultData.description;
    originalVault.img = vaultData.img ?? vaultData.img;
    return _vaultsRepo.UpdateVault(originalVault);
  }




  public void DeleteVault(int vaultId, string userId)
  {
    var vault = GetByVaultId(vaultId);
    if (vault.creatorId != userId)
    {
      throw new Exception("Insufficient Permission To Delete");
    }
    _vaultsRepo.Delete(vaultId);
  }





  internal Vault GetByVaultId(int id)
  {
    var foundVault = _vaultsRepo.GetVaultById(id);
    if (foundVault == null)
    {
      throw new Exception("This vault does not exist");
    }
    return foundVault;
  }



  internal List<VaultKeepViewModel> GetVaultKeepsByVaultId(int vaultId)
  {
    return _vaultsRepo.getVaultKeepsByVaultId(vaultId);
  }







  public List<Vault> GetMyVaults(string creatorId)
  {
    return _vaultsRepo.GetMyVaults(creatorId);
  }













}