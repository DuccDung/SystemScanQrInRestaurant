using Microsoft.AspNetCore.Mvc;
using Server_QR.Models;
using Server_QR.Services;

namespace Server_QR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly QlnhaHangBtlContext _context;
        private readonly IApiService _apiSevice;
        public UsersController(QlnhaHangBtlContext context , IApiService apiService)
        {
            _context = context;
            _apiSevice = apiService;
        }
        [Route("InitUser")]
        [HttpGet]
        public async Task<IActionResult> InitUser(string userName)
        {
            return Ok( await _apiSevice.CreateUser(userName));
        }
           
        [Route("GetUserInformation")]
        [HttpGet]
        public async Task<IActionResult> GetUserInformation(int userId)
        {
            return Ok(await _apiSevice.GetUserInformation(userId.ToString()));
        }

        [Route("CheckUserInformation")]
        [HttpGet]
        public async Task<IActionResult> CheckUserAlreadyExists(int userId)
        {
            return Ok(await _apiSevice.CheckUserAlreadyExists(userId));
        }
    }
}
