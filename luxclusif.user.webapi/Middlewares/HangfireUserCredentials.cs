﻿using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace luxclusif.user.webapi.Middlewares
{
    public class HangfireUserCredentials
    {
        public HangfireUserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public byte[]? PasswordSha1Hash { get; set; }

        public string Password
        {
            set
            {
                using var cryptoProvider = SHA1.Create();
                PasswordSha1Hash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            if (username == Username)
            {
                using var cryptoProvider = SHA1.Create();
                byte[] passwordHash = cryptoProvider.ComputeHash(Encoding.UTF8.GetBytes(password));
                return StructuralComparisons.StructuralEqualityComparer.Equals(passwordHash, PasswordSha1Hash);
            }
            else
                return false;
        }
    }
}
