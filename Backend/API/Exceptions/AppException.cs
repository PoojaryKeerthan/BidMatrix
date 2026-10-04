/*
* =================================================================================
* Project:       BidMatrix/API
* File:          AppException.cs
* Description:   Base class for the app handled exceptions.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-03   Keerthan P Poojary   Implemented the base exception class.
* ==================================================================================
*/
using System;

namespace API.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode { get; set; }
        public AppException(string message,int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
