namespace TheKeep.Controllers;
[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly VaultsService _vs;
  public VaultsController(Auth0Provider auth0provider, VaultsService @vs)
  {
    _auth0provider = auth0provider;
    _vs = @vs;
  }



  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newVault.creatorId = userInfo.Id;
      Vault createdVault = _vs.CreateVault(newVault);
      createdVault.Creator = userInfo;
      return Ok(createdVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVault(vaultId, userInfo.Id);
      return Ok("Vault Deleted!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPut("{id}")]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = id;
      Vault vault = _vs.UpdateVault(vaultData, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }










  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetByVaultId(int vaultId)
  {
    try
    {
      Vault foundVault = _vs.GetByVaultId(vaultId);
      if (foundVault.isPrivate == true)
      {
        return BadRequest("This Vault is Private!!!");
      }
      return Ok(foundVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{vaultId}/keeps")]
  // [Authorize]
  public ActionResult<List<VaultKeepViewModel>> GetVaultKeepsByVaultId(int vaultId)
  {
    try
    {
      // Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      List<VaultKeepViewModel> vaultKeeps = _vs.GetVaultKeepsByVaultId(vaultId);
      Vault foundVault = _vs.GetByVaultId(vaultId);
      if (foundVault.isPrivate)
      {
        return BadRequest("This vault is private!");
      }
      return Ok(vaultKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }






}
