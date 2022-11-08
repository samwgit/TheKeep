namespace TheKeep.Repositories;

public class VaultsRepository : BaseRepository
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }




  internal Vault CreateVault(Vault newVault)
  {
    string sql = @"
  INSERT INTO vaults(creatorId, name, description, img, isPrivate)
  VALUES(@creatorId, @name, @description, @img, @isPrivate);
  SELECT LAST_INSERT_ID()
  ;";
    int id = _db.ExecuteScalar<int>(sql, newVault);
    newVault.Id = id;
    return newVault;
  }

  public Vault UpdateVault(Vault originalVault)
  {
    var sql = @"
    UPDATE vaults
    SET
    name = @name,
    description = @description,
    img = @img
    ;"; _db.Execute(sql, originalVault);
    return originalVault;
  }


  internal void Delete(int id)
  {
    var sql = "DELETE FROM vaults where id = @Id LIMIT 1;";
    var rows = _db.Execute(sql, new { id });
    if (rows != 1)
    {
      throw new Exception("v.row error");
    }
    return;
  }








  public Vault GetVaultById(int vaultId)
  {
    string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a ON a.id = v.creatorId
  WHERE v.id = @vaultId
  ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
  }



  internal List<VaultKeepViewModel> getVaultKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT
k.*,
vk.*,
a.*
FROM vaultKeeps vk
JOIN keeps k ON k.id = vk.keepId
JOIN accounts a ON a.id = k.creatorId
WHERE vk.vaultId = @vaultId
;";
    return _db.Query<VaultKeepViewModel, VaultKeep, Profile, VaultKeepViewModel>(sql, (vkvm, vk, profile) =>
    {
      vkvm.Creator = profile;
      vkvm.vaultKeepId = vk.Id;
      return vkvm;
    }, new { vaultId }).ToList();
  }



  public List<Vault> GetMyVaults(string creatorId)
  {
    var sql = @"
      SELECT 
        vault.*,
        a.*
      FROM vaults vault
      JOIN accounts a ON a.id = vault.creatorId
      WHERE vault.creatorId = @creatorId
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, profile) =>
    {
      v.Creator = profile;
      return v;
    }, new { creatorId }).ToList();
  }




}