namespace TheKeep.Controllers;
[ApiController]
[Route("api/[controller]")]

public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly KeepsService _ks;
  public KeepsController(Auth0Provider auth0provider, KeepsService @ks)
  {
    _auth0provider = auth0provider;
    _ks = @ks;
  }







  // NOTE Creating, Deleting, & Editing Keeps
  #region Authorized Routes

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep newKeep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      newKeep.creatorId = userInfo.Id;
      Keep createdKeep = _ks.CreateKeep(newKeep);
      createdKeep.Creator = userInfo;
      return Ok(createdKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep keepData, int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = id;
      Keep keep = _ks.UpdateKeep(keepData, userInfo.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpPut("{id}/views")]
  [Authorize]
  public async Task<ActionResult<Keep>> incrementKeepView([FromBody] Keep keep)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      Keep keepViewed = _ks.incrementKeepViews(keep, keep.Id);
      return Ok(keepViewed);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      var userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _ks.DeleteKeep(keepId, userInfo.Id);
      return Ok("Keep Deleted!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  #endregion



  // NOTE Getting All & by Id
  #region Unauthorized Routes

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _ks.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetByKeepId(int keepId)
  {
    try
    {
      Keep foundKeep = _ks.GetByKeepId(keepId);
      return Ok(foundKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  #endregion
}