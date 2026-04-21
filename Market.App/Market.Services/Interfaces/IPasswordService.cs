using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Services.Interfaces
{
    internal interface IPasswordService
    {
        Task<bool> VerifyPasswordAsync(string authId, string password);
        Task SetPasswordAsync(string authId, string password);
    }
}
