﻿//using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AssessmentSystem.MVC.Utils
{
    public class LoginManager
    {
        //public static string GetHashPassword(string password)
        //{
        //    byte[] salt = new byte[128 / 8];
        //    using (var rngCsp = new RNGCryptoServiceProvider())
        //    {
        //        rngCsp.GetNonZeroBytes(salt);
        //    }
            
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 100000,
        //        numBytesRequested: 256 / 8));

        //    return hashed;
        //}
    }
}