namespace WhatShouldIDoNow.Services
{
    public interface IHashingWrapper
    {
        string HashPassword(string password);
        bool Verify(string password, string hash);
        bool IsBcryptHash(string possibleHash);
    }
}
