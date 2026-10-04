/*
* =================================================================================
* Project:       BidMatrix/API
* File:          AuthDataHandler.cs
* Description:   Data handler file for the user register/login/auth.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-02   Keerthan P Poojary   Implemented the Datahandling for the auth.
* ==================================================================================
*/
using API.BuisnessLogics;
using API.Logging;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace API.DataHandler
{
    public class AuthDataHandler
    {
        #region Members
        private readonly AppDbContext _context;
        private readonly IAppLogger log;
        #endregion
        #region Constructors
        public AuthDataHandler(AppDbContext context,IAppLogger logger)
        {
            this._context = context;
            this.log = logger;
        }
        #endregion
        #region Methods
        public async Task<bool> CheckUserExists(string username, string email)
        {
            log.LogMethodEntry(username, email);
            bool result = await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
            log.LogMethodExit(result);
            return result;
        }
        public async Task RegisterUser(User user)
        {
            log.LogMethodEntry(user);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            log.LogMethodExit("User registered successfully");
        }
        public async Task<User?>GetUserByEmail(string email)
        {
            log.LogMethodEntry(email);
            log.LogMethodExit();
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        #endregion
    }
}
