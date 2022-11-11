
namespace TheKeep.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }



  internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep)
  {
    string sql = @"
  INSERT INTO vaultKeeps(creatorId, vaultId, keepId)
  VALUES(@creatorId, @vaultId, @keepId);
  SELECT LAST_INSERT_ID()
  ;";

    int id = _db.ExecuteScalar<int>(sql, newVaultKeep);
    string sql2 = "UPDATE keeps SET kept = kept + 1 WHERE id = @keepId;";
    int keepId = newVaultKeep.keepId;
    int id2 = _db.Execute(sql2, new { keepId });
    newVaultKeep.Id = id;
    // if (newVaultKeep.Creator == null)
    // {
    //   throw new Exception("Creator is null");
    // }
    return newVaultKeep;
  }



  internal void DeleteVaultKeep(int id)
  {
    var sql = @"
    DELETE FROM vaultKeeps vk WHERE vk.id = @Id LIMIT 1;";
    var rows = _db.Execute(sql, new { id });
    if (rows != 1)
    {
      throw new Exception("vk.row error");
    }
    return;
  }

  public VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
  SELECT
  vk.*,
  a.*
  FROM vaultKeeps vk
  JOIN accounts a ON a.id = vk.creatorId
  WHERE vk.id = @vaultKeepId
  ;";
    return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) =>
    {
      vaultKeep.Creator = profile;
      return vaultKeep;
    }, new { vaultKeepId }).FirstOrDefault();
  }




  // public List<VaultKeep> GetAllVaultKeeps(string userId)
  // {
  //   string sql = @"
  //   SELECT 
  //   vk.*,
  //   FROM vaultKeeps vk
  //   WHERE vk.creatorId = @userId
  //   ;";
  //   return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vaultKeep, profile) =>
  //   {
  //     vaultKeep.Creator = profile;
  //     return vaultKeep;
  //   }, new { userId }).ToList();
  // }
}