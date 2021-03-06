﻿using Daifuku.Exceptions;
using Daifuku.Validations;
using System;
using System.Collections.Generic;

namespace Daifuku.SampleWeb.Exceptions
{
    public class AppException : Exception, IApplicationException
    {
        public IEnumerable<ValidationError> Messages { get; }

        public AppException()
        {
        }

        public AppException(string message) : base(message)
        {
        }

        public AppException(IEnumerable<ValidationError> messages)
        {
            Messages = messages;
        }
    }
}