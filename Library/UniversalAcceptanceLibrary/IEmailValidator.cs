﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalAcceptanceLibrary
{
    /// <summary>
    /// Implements EAI-compatible email validation.
    /// See RFC 5321, 5322, 6532.
    /// </summary>
    public interface IEmailValidator
    {
        bool IsValidEmail(string email);
    }
}