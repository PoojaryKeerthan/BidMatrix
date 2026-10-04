/*
* =================================================================================
* Project:       BidMatrix/API
* File:          AuthBusinessLogic.cs
* Description:   Business logics and data validation for the auth.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-02   Keerthan P Poojary   implemented the auth business logics and validation.
* ==================================================================================
*/
using API.DataHandler;
using API.Dtos.RequestDtos;
using API.Dtos.ResponseDtos;
using API.Exceptions;
using API.Logging;
using API.Models;
using API.Models.Enums;
using API.Services.Jwtservices;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace API.BLFiles
{
    public class AuthBusinessLogic
    {
        #region Members
        private readonly IAppLogger log;
        private readonly AuthDataHandler userDataHandler;
        private readonly IJwtService jwtService;
        #endregion
        #region Constructor
        public AuthBusinessLogic(IAppLogger appLogger,AuthDataHandler userDataHandler,IJwtService jwtService)
        {
            this.log = appLogger;
            this.userDataHandler = userDataHandler;
            this.jwtService = jwtService;
        }
        #endregion
        #region Methods
        public async Task CreateUser(RegisterRequestDto registerRequestDto)
        {
            log.LogMethodEntry(registerRequestDto);
            bool userExists = await userDataHandler.CheckUserExists(registerRequestDto.Username, registerRequestDto.Email.ToLower());
            if (userExists)
            {
                log.LogMethodExit();
                throw new UserAlreadyExistsException("user already exists try using different username or email.");
            }
            string passWordHash = BCrypt.Net.BCrypt.HashPassword(registerRequestDto.Password);
            User newUser = new User(registerRequestDto.Username,registerRequestDto.Email.ToLower(),passWordHash,UserRole.User);
            await userDataHandler.RegisterUser(newUser);
            log.LogMethodExit();
        } 
        public async Task<LoginResponseDto> LoginUser(LoginRequestDto requestUser)
        {
            log.LogMethodEntry();
            User? user = await userDataHandler.GetUserByEmail(requestUser.Email.ToLower());
            if(user == null)
            {
                throw new InvalidCredentialsException("Invalid email or password.");
            }
            bool isPassWordValid = BCrypt.Net.BCrypt.Verify(requestUser.Password, user.PasswordHash);
            if (isPassWordValid == false)
            {
                throw new InvalidCredentialException("Invalid email or password.");
            }
            string token = jwtService.GenerateToken(user);
            log.LogMethodExit();
            return new LoginResponseDto(user.Username, user.Email, user.Role.ToString(), token);
        }
        #endregion
        #region Private Methods
        #endregion
    }
}
