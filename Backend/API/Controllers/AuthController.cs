/*
* =================================================================================
* Project:       BidMatrix/API
* File:          AuthController.cs
* Description:   Controller for the auth.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-02   Keerthan P Poojary   Implemented the auth controller for the register    
*                                   and login actions.
* ==================================================================================
*/
using API.BLFiles;
using API.Dtos.RequestDtos;
using API.Dtos.ResponseDtos;
using API.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Members 
        private readonly IAppLogger log;
        private readonly AuthBusinessLogic authBusinessLogic;
        #endregion
        #region Constructors
        public AuthController(IAppLogger appLogger,AuthBusinessLogic authBusinessLogic)
        {
            this.log = appLogger;
            this.authBusinessLogic = authBusinessLogic;
        }
        #endregion
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            log.LogMethodEntry(request);
            await authBusinessLogic.CreateUser(request);
            return Ok("user created successfully.");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            log.LogMethodEntry();
            LoginResponseDto result = await authBusinessLogic.LoginUser(requestDto);
            Response.Cookies.Append("access_token", result.Token!, new CookieOptions { HttpOnly = true, Secure = true });
            result.Token = string.Empty;
            return Ok(new ApiResponseDto<LoginResponseDto>(true, "Login successfull.", StatusCodes.Status200OK, result));
        }
    }
}
