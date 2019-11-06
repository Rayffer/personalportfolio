using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.HashingProviders
{
    public class DummyHashingProvider : IHashingProvider
    {
        public bool CheckFilePerform(string key, string value)
        {
            return true;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            return input;
        }

        private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            return true;
        }
    }
}