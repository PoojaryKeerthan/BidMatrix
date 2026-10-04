/*
* =================================================================================
* Project:       BidMatrix/API
* File:          AppException.cs
* Description:   Global exception handler class.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-03   Keerthan P Poojary   Implemented the global exception class.
* ==================================================================================
*/
using API.Dtos.ResponseDtos;
using API.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        #region Members
        private readonly IAppLogger log;
        private readonly IWebHostEnvironment environment;
        #endregion
        #region Constructor
        public GlobalExceptionHandler(IAppLogger appLogger,IWebHostEnvironment environment)
        {
            this.log = appLogger;
            this.environment = environment;
        }
        #endregion
        #region Methods
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            log.LogException(exception);
            int statusCode;
            string message;
            string title;
            if (exception is AppException appException)
            {
                statusCode = appException.StatusCode;
                message = appException.Message;
                title = "Request failed";
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = environment.IsDevelopment() ? exception.Message : "An unexpected error occurred from the server.";
                title = "Internal Server Error";
            }
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Detail = message,
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = statusCode;
            ApiResponseDto<object> response = new ApiResponseDto<object>(false, message, statusCode, null, problemDetails);
            await httpContext.Response.WriteAsJsonAsync(response,cancellationToken);
            return true;
        }
        #endregion
    }
}
