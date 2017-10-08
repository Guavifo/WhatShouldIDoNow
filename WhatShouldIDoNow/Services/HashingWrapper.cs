using System.Text.RegularExpressions;

namespace WhatShouldIDoNow.Services
{
    public class HashingWrapper : IHashingWrapper
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public bool IsBcryptHash(string possibleHash)
        {
            const string pattern = @"^\$2[ayb]\$.{56}$";
            var result = Regex.IsMatch(possibleHash, pattern);

            return result;
        }
    }
}
