using Store.BusinessLogic.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Store.BusinessLogic.Providers
{
    public class PasswordGeneratorProvider : IPasswordGeneratorProvider
    {
        public string RandomPasswordGenerator(int length)
        {
            return GetRandomString(length, Shared.Constants.DefaultValues.alphanumericCharacters);
        }

        public string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            var characterArray = characterSet.Distinct().ToArray();

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
