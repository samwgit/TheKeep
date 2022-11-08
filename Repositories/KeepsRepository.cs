namespace TheKeep.Repositories;

public class KeepsRepository : BaseRepository
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }



  internal Keep CreateKeep(Keep newKeep)
  {
    string sql = @"
    INSERT INTO keeps(creatorId, name, description, img, views, kept)
    VALUES(@creatorId, @name, @description, @img, @views, @kept);
    SELECT LAST_INSERT_ID()
    ;";
    int id = _db.ExecuteScalar<int>(sql, newKeep);
    newKeep.Id = id;
    return newKeep;
  }

  public List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT 
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId 
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }
    ).ToList();
  }

  public Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT 
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.id = @keepId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { keepId }).FirstOrDefault();
  }

  public Keep UpdateKeep(Keep originalKeep)
  {
    var sql = @"
    UPDATE keeps
    SET
    name = @name,
    description = @description,
    img = @img
    ;"; _db.Execute(sql, originalKeep);
    return originalKeep;
  }

  internal void Delete(int id)
  {
    var sql = "DELETE FROM keeps where id = @Id LIMIT 1;";
    var rows = _db.Execute(sql, new { id });
    if (rows != 1)
    {
      throw new Exception("k.row error");
    }
    return;
  }

}