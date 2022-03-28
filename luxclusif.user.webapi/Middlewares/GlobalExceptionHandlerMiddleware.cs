﻿using luxclusif.user.application.Constants;
using luxclusif.user.application.Exceptions;
using luxclusif.user.application.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace luxclusif.user.webapi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro de processamento | {context.Request.Path.Value}");
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado | {context.Request.Path.Value}");
                await HandleExceptionAsync(context);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, BusinessException exception)
        {
            return HandleErrorResponseAsync(context,
                exception.StatusCode,
                exception.ContentType,
                exception.ErrorCode);
        }

        private Task HandleExceptionAsync(HttpContext context)
        {
            return HandleErrorResponseAsync(context,
                HttpStatusCode.InternalServerError,
                MediaTypeNames.Application.Json,
                ErrorCodeConstant.Generic);
        }

        private Task HandleErrorResponseAsync(
            HttpContext context,
            HttpStatusCode httpStatusCode,
            string mediaType,
            ErrorModel errorCode)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = mediaType;

            var response = new DefaultResponseDto<object>();

            response.Errors.Add(errorCode);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
