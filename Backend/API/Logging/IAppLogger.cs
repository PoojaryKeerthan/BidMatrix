/*
* =====================================================================================
* Project:       BidMatrix/API
* File:          IAppLogger.cs
* Description:   Handles contracts of logger class to implement.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  -------------------------------------------------------
* 2026-09-30   Keerthan P Poojary        Created initial setup of logging and log 
*                                        methods.
* =====================================================================================
*/
using System;

namespace API.Logging
{
    public interface IAppLogger
    {
        void LogClassEntry();
        void LogMethodEntry(params object[] parameters);
        void LogMethodExit(object? result);
        void LogException(Exception exception);
        void Info(string message);
    }
}
