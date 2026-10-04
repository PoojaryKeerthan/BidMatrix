/*
* =================================================================================
* Project:       BidMatrix/API
* File:          UserAlreadyExistsException.cs
* Description:   Exception handling for the duplicate user.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-03   Keerthan P Poojary   Implemented the auth exceptions for the register    
*                                   and login actions.
* ==================================================================================
*/
using Microsoft.AspNetCore.Http;
using System;

namespace API.Exceptions
{
    public class UserAlreadyExistsException : AppException
    {
        public UserAlreadyExistsException(string message) : base(message,StatusCodes.Status409Conflict)
        {                
        }
    }
}
