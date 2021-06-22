using Shared.Constants;
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
            return GetRandomString(length, DefaultValues.alphanumericCharacters);
        }

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            var characterArray = characterSet.Distinct().ToArray();

            var bytes = new byte[length * DefaultValues.PasswordCount];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = default; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * DefaultValues.PasswordCount);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
