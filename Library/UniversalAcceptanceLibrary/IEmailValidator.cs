using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAcceptanceLibrary
{
    /// <summary>
    /// Implements EAI-compatible email validation.
    /// See RFC 5321, 5322, 6532.
    /// </summary>
    public interface IEmailValidator
    {
        Task<bool> IsValidEmailAsync(string email);
    }
}
