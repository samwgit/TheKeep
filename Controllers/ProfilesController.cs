namespace TheKeep.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;

  private readonly Auth0Provider _auth0provider;

  private readonly VaultsService _vs;

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0provider, VaultsService vs)
  {
    _profilesService = profilesService;
    _auth0provider = auth0provider;
    _vs = vs;
  }

  [HttpGet("{id}")]
  public ActionResult<Profile> Get(string id)
  {
    try
    {
      return Ok(_profilesService.GetProfile(id));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }






  [HttpGet("{id}/keeps")]
  public ActionResult<Profile> getUserKeeps(string id)
  {
    try
    {
      return Ok(_profilesService.getUserKeeps(id));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}/vaults")]
  public ActionResult<Vault> getUserVaults(string id)
  {
    try
    {
      return Ok(_profilesService.getUserVaults(id));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }














}
