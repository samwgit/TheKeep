namespace TheKeep.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }




  internal Profile GetById(string id)
  {
    string sql = "SELECT * FROM accounts WHERE id = @id";
    return _db.QueryFirstOrDefault<Profile>(sql, new { id });
  }


  public List<Keep> getUserKeeps(string id)
  {
    string sql = @"
    SELECT 
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId 
    WHERE creatorId = @id 
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { id }).ToList();
  }

  public List<Vault> getUserVaults(string id)
  {
    string sql = @"
    SELECT 
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.id = v.creatorId 
    WHERE creatorId = @id 
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { id }).ToList();
  }




  // internal Profile Update(Profile update)
  // {
  //   string sql = @"
  //   UPDATE accounts
  //   SET 
  //   name = @Name,
  //   picture = @Picture
  //   WHERE id = @Id
  //   ;";
  //   _db.Execute(sql, update);
  //   return update;
  // }
}