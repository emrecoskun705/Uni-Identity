﻿using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using UniIdentity.Domain.Credentials.Services;
using UniIdentity.Domain.Users.ValueObjects;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace UniIdentity.Infrastructure.Cryptography;

internal sealed class PasswordHasher : IPasswordHasher, IPasswordVerifier, IDisposable
{
    private const KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;
    private const int IterationCount = 10000;
    private const int NumberOfBytesRequested = 256 / 8;
    private const int SaltSize = 128 / 8;
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public string HashPassword(Password password)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        string hashedPassword = Convert.ToBase64String(HashPasswordInternal(password));

        return hashedPassword;
    }

    public bool VerifyHashedPassword(string hashedPassword, Password password)
    {
        ArgumentNullException.ThrowIfNull(hashedPassword);

        ArgumentNullException.ThrowIfNull(password);

        var decodedHashedPassword = Convert.FromBase64String(hashedPassword);

        if (decodedHashedPassword.Length == 0)
        {
            return false;
        }

        var verified = VerifyPasswordHashInternal(decodedHashedPassword, password);

        return verified;
    }

    /// <summary>
    /// Returns the bytes of the hash for the specified password.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>The bytes of the hash for the specified password.</returns>
    private byte[] HashPasswordInternal(string password)
    {
        var salt = GetRandomSalt();

        var subKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, NumberOfBytesRequested);

        var outputBytes = new byte[salt.Length + subKey.Length];

        Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);

        Buffer.BlockCopy(subKey, 0, outputBytes, salt.Length, subKey.Length);

        return outputBytes;
    }

    /// <summary>
    /// Gets a randomly generated salt.
    /// </summary>
    /// <returns>The randomly generated salt.</returns>
    private byte[] GetRandomSalt()
    {
        byte[] salt = new byte[SaltSize];

        _rng.GetBytes(salt);

        return salt;
    }

    /// <summary>
    /// Verifies the bytes of the hashed password with the specified password.
    /// </summary>
    /// <param name="hashedPassword">The bytes of the hashed password.</param>
    /// <param name="password">The password to verify with.</param>
    /// <returns>True if the hashes match, otherwise false.</returns>
    private static bool VerifyPasswordHashInternal(byte[] hashedPassword, string password)
    {
        try
        {
            var salt = new byte[SaltSize];

            Buffer.BlockCopy(hashedPassword, 0, salt, 0, salt.Length);

            var subKeyLength = hashedPassword.Length - salt.Length;

            if (subKeyLength < SaltSize)
            {
                return false;
            }

            var expectedSubKey = new byte[subKeyLength];

            Buffer.BlockCopy(hashedPassword, salt.Length, expectedSubKey, 0, expectedSubKey.Length);

            var actualSubKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, subKeyLength);

            return ByteArraysEqual(actualSubKey, expectedSubKey);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Returns true if the specified byte arrays are equal, otherwise false.
    /// </summary>
    /// <param name="a">The first byte array.</param>
    /// <param name="b">The second byte array.</param>
    /// <returns>True if the arrays are equal, otherwise false.</returns>
    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null)
        {
            return true;
        }

        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        var areSame = true;

        for (var i = 0; i < a.Length; i++)
        {
            areSame &= a[i] == b[i];
        }

        return areSame;
    }

    public void Dispose()
    {
        _rng.Dispose();
    }
}