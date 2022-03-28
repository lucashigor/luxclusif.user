﻿using luxclusif.user.application.Models;
using System.Net;
using System.Net.Mime;

namespace luxclusif.user.application.Exceptions
{
    public class BusinessException : Exception
    {
        public ErrorModel ErrorCode { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string ContentType { get; set; } = MediaTypeNames.Application.Json;

        public BusinessException(ErrorModel errorCode) : base(errorCode.Message)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(ErrorModel errorCode, Exception inner) : base(errorCode.Message, inner)
        {
            ErrorCode = errorCode;
        }
    }
}
