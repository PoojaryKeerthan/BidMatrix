/*
* =================================================================================
* Project:       BidMatrix/API
* File:          AppLogger.cs
* Description:   Implements the log methods.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-09-30   Keerthan P Poojary        Created initial setup of logging and log 
*                                        methods.
* ==================================================================================
*/
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace API.Logging
{
    public class AppLogger : IAppLogger
    {
        #region Private Members
        private readonly ILogger<AppLogger> _logger;
        #endregion
        #region Constructor
        public AppLogger(ILogger<AppLogger> logger)
        {
            _logger = logger;
        }
        #endregion
        #region Methods
        public void LogClassEntry()
        {
            StackFrame? frame = new StackFrame(1);
            MethodBase? method = frame.GetMethod();
            string? className = method?.DeclaringType?.FullName;
            WriteLog("INFO", $"{className} | Class Entry");
            _logger.LogInformation("{ClassName | Class Entry} ", className);
        }
        public void LogMethodEntry(params object[] parameters)
        {
            var res = GetCallerInfo();
            string parameterValues = parameters.Length > 0 ? string.Join(", ", parameters): "None";
            WriteLog("INFO",$"{res.ClassName} | {res.MethodName} | Method Entry | Parameters: {parameterValues}");
            _logger.LogInformation("{ClassName} | {MethodName} | Method Entry | Parameters: {Parameters}", res.ClassName, res.MethodName, parameterValues);
        }
        public void LogMethodExit(object? result)
        {
            var res = GetCallerInfo();
            WriteLog("INFO",$"{res.ClassName} | {res.MethodName} | Method Exit | Result: {result}");
            _logger.LogInformation("{ClassName} | {MethodName} | Method Exit | Result: {Result}", res.ClassName, res.MethodName, result);
        }
        public void Info(string message)
        {
            var res = GetCallerInfo();
            WriteLog("INFO",$"{res.ClassName} | {res.MethodName} | {message}");
            _logger.LogInformation("{Message}", message);
        }
        public void LogException(Exception exception)
        {
            var res = GetCallerInfo();
            WriteLog("ERROR",$"{res.ClassName} | {res.MethodName} | Exception: {exception.Message}");
            _logger.LogError(exception,"{ClassName} | {MethodName} | Exception",res.ClassName,res.MethodName);
        }
        #endregion
        #region Private Methods
        private (string? ClassName,string MethodName) GetCallerInfo()
        {
            StackFrame? frame = new StackFrame(2);
            MethodBase? method = frame.GetMethod();
            string? className = method?.DeclaringType?.FullName;
            string methodName = method?.Name ?? "Unknown";
            return (className, methodName);
        }
        private void WriteLog(string level, string message)
        {
            string logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDirectory);
            string logFile = Path.Combine(logDirectory,$"{DateTime.Now:yyyy-MM-dd}.log");
            string logMessage =$"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {level} | {message}";
            File.AppendAllText(logFile, logMessage + Environment.NewLine);
        }
        #endregion
    }
}
