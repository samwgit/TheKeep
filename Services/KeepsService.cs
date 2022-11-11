namespace TheKeep.Services;

public class KeepsService
{
  private readonly KeepsRepository _keepsRepo;



  public KeepsService(KeepsRepository repo)
  {
    _keepsRepo = repo;
  }

  internal Keep CreateKeep(Keep newKeep)
  {
    return _keepsRepo.CreateKeep(newKeep);
  }

  internal List<Keep> GetAllKeeps()
  {
    return _keepsRepo.GetAllKeeps();
  }

  internal Keep GetByKeepId(int id)
  {
    var foundKeep = _keepsRepo.GetKeepById(id);
    if (foundKeep == null)
    {
      throw new Exception("This keep does not exist");
    }
    return foundKeep;
  }

  public Keep UpdateKeep(Keep keepData, string userId)
  {
    Keep originalKeep = GetByKeepId(keepData.Id);
    if (originalKeep.creatorId != userId)
    {
      throw new Exception("Insufficient Permission To Edit");
    }
    originalKeep.name = keepData.name ?? keepData.name;
    originalKeep.description = keepData.description ?? keepData.description;
    originalKeep.img = keepData.description ?? keepData.description;
    return _keepsRepo.UpdateKeep(originalKeep);
  }

  public void DeleteKeep(int keepId, string userId)
  {
    var keep = GetByKeepId(keepId);
    if (keep.creatorId != userId)
    {
      throw new Exception("Insufficient Permission To Delete!");
    }
    _keepsRepo.Delete(keepId);
  }



  public Keep incrementKeepViews(Keep keep, int id)
  {
    _keepsRepo.incrementKeepViews(keep, id);
    return keep;
  }




}