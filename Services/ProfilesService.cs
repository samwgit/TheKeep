namespace TheKeep.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }






  internal Profile GetProfile(string id)
  {
    Profile profile = _repo.GetById(id);
    return profile;
  }


  internal List<Keep> getUserKeeps(string id)
  {
    Profile profile = _repo.GetById(id);
    return _repo.getUserKeeps(id);
  }

  internal List<Vault> getUserVaults(string id)
  {

    // Profile profile = _repo.GetById(id);
    if (id == null)
    {
      throw new Exception("Test");
    }
    List<Vault> vaults = _repo.getUserVaults(id);

    return vaults.FindAll(v => v.isPrivate == false || v.creatorId != id);
  }



  // internal Profile Update(Profile accountData, string userId)
  // {
  //   Profile user = _repo.GetById(accountData.Id);
  //   user.Name = accountData.Name ?? accountData.Name;
  //   user.Picture = accountData.Picture ?? accountData.Picture;
  //   return _repo.Update(user);
  // }
}
