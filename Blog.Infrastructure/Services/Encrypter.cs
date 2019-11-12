using Blog.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Blog.Common.Extensions;

namespace Blog.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private readonly EncryptionSettings _settings;

        public Encrypter(EncryptionSettings settings)
        {
            _settings = settings;
        }

        public string GetSalt(string value)
        {
            if (value.Empty())
                throw new ArgumentException("Can not generate salt from an empty value.", nameof(value));

            var saltBytes = new byte[_settings.SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if (value.Empty())
                throw new ArgumentException("Can not generate hash from an empty value.", nameof(value));
            if (salt.Empty())
                throw new ArgumentException("Can not use an empty salt from hashing value.", nameof(value));

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), _settings.DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(_settings.SaltSize));
        }

        private byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
