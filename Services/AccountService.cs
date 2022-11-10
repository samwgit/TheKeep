namespace TheKeep.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account accountData, string id)
  {
    Account original = GetProfileByEmail(id);
    original.Name = accountData.Name.Length > 0 ? accountData.Name : original.Name;
    original.Picture = accountData.Picture.Length > 0 ? accountData.Picture : original.Picture;
    return _repo.Edit(original);
  }



}
