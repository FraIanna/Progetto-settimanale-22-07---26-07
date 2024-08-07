﻿using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Implementation.PasswordEncoder
{
    public class RSAPasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {
            using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            var enc = rsa.Encrypt(Encoding.UTF8.GetBytes(password), RSAEncryptionPadding.OaepSHA256);
            return Convert.ToBase64String(enc);
        }

        public bool IsSame(string plainText, string codedText)
        {
            return Encode(plainText) == codedText;
        }
    }
}
