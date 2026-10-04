/*
* =================================================================================
* Project:       BidMatrix/API
* File:          ApiResponseDto.cs
* Description:   standard dto class for api response.
* 
* Revision History:
* Date         Author          Description
* -----------  --------------  ----------------------------------------------------
* 2026-10-03   Keerthan P Poojary   Implemented a standard response dto class for
*                                   both error and success responses.
* ==================================================================================
*/
using Microsoft.AspNetCore.Mvc;

namespace API.Dtos.ResponseDtos
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }

        public ApiResponseDto(bool success,string message,int statusCode, T? data = default, ProblemDetails? problem = null)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Data = data;
            ProblemDetails = problem;
        }
    }
}
