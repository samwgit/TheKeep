namespace TheKeep.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  private readonly VaultsService _vs;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService @vs)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vs = @vs;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{vaults}")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      var userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      var myVaults = _vs.GetMyVaults(userInfo?.Id);

      return Ok(myVaults);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




  [HttpPut]
  [Authorize]

  public async Task<ActionResult<Account>> UpdateAccount([FromBody] Account accountData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      // accountData.Id = id;
      Account account = _accountService.Edit(accountData, accountData.Email);
      return Ok(account);
    }
    catch (System.Exception)
    {
      throw;
    }
  }











}
