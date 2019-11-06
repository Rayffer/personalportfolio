using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Tracing;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.HashingProviders
{
    public class Md5HashingProvider : IHashingProvider
    {
        private readonly ITracing myITracing;

        public Md5HashingProvider(ITracingFactory tracingFactory)
        {
            myITracing = (tracingFactory != null) ? tracingFactory.GetTracing(this.GetType()) : new Log4NetTracing(this.GetType());
        }

        public bool CheckFilePerform(string key, string value)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, value);

                myITracing.Information(string.Format("The MD5 hash of {0} is {1}.", value, hash));
                myITracing.Information(string.Format("Verifying the hash..."));

                if (VerifyMd5Hash(md5Hash, value, key))
                {
                    myITracing.Information(string.Format("The hashes are the same."));
                    return true;
                }
                else
                {
                    myITracing.Information(string.Format("The hashes are not same."));
                    return false;
                }
            }
        }

        internal string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        internal bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}