using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Data.Interfaces;

namespace Uzumachi.McBuilds.Api.Controllers {

  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase {

    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IUnitOfWork unitOfWork) {
      _unitOfWork = unitOfWork;
    }

    [HttpGet("[action]")]
    public IActionResult Ping() {
      return Ok(new { message = "Pong" });
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll() {

      var users = await _unitOfWork.Users.GetAll();

      return Ok(users);
    }

  }
}
