[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly VaultKeepsService _vks;
  private readonly KeepsService _ks;
  public VaultKeepsController(Auth0Provider auth0provider, VaultKeepsService @vks, KeepsService @ks)
  {
    _auth0provider = auth0provider;
    _vks = @vks;
    _ks = @ks;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newVaultKeep.creatorId = userInfo.Id;
      VaultKeep createdVaultKeep = _vks.CreateVaultKeep(newVaultKeep, userInfo.Id);

      createdVaultKeep.Creator = userInfo;
      // if (createdVaultKeep.creatorId != userInfo.Id)
      // {
      //   throw new Exception("Test");
      // }
      return Ok(createdVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [HttpGet("{vaultKeepId}")]
  public ActionResult<VaultKeep> GetByVaultKeepId(int vaultKeepId)
  {
    try
    {
      VaultKeep foundVaultKeep = _vks.GetByVaultKeepId(vaultKeepId);
      return Ok(foundVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  // [HttpGet]
  // [Authorize]
  // public async Task<ActionResult<List<VaultKeep>>> GetAllVaultKeeps()
  // {
  //   try
  //   {
  //     var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
  //     List<VaultKeep> keeps = _vks.GetAllVaultKeeps(userInfo.Id);
  //     return Ok(keeps);
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }

  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vks.DeleteVaultKeep(vaultKeepId, userInfo.Id);
      return Ok("Vault Keep Deleted!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



}