using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Services.Abstract
{
    public interface IEncryptionService
    {
        public string GenerateRandomIV(int size);
        public string AESEncrypt(string plainText);
        public string AESDecrypt(string plainText);
        public string Base64Encode(string plainText);
        public string Base64Decode(string plainText);
        public string MD5Encrypt(string plainText);
    }
}
