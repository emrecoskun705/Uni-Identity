namespace UniIdentity.Domain.Configs.Enums;

/// <summary>
/// Represents the intended use of a cryptographic key.
/// </summary>
public record KeyUse
{
    public string Value { get; }

    private KeyUse(string value) => Value = value;

    /// <summary>
    /// Gets a <see cref="KeyUse"/> instance representing the 'SIG' key use.
    /// </summary>
    public static KeyUse Sig => new KeyUse("SIG");

    /// <summary>
    /// Gets a <see cref="KeyUse"/> instance representing the 'ENC' key use.
    /// </summary>
    public static KeyUse Enc => new KeyUse("ENC");

    /// <summary>
    /// Implicitly converts a string value to a <see cref="KeyUse"/> instance.
    /// </summary>
    /// <param name="value">The string value representing the key use.</param>
    /// <returns>A <see cref="KeyUse"/> instance corresponding to the provided string value.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided string value is invalid.</exception>
    public static implicit operator KeyUse(string value) => value switch
    {
        "SIG" => Sig,
        "ENC" => Enc,
        _ => throw new ArgumentException($"Invalid value '{value}' for KeyUse"),
    };

    /// <summary>
    /// Returns the string representation of the key use.
    /// </summary>
    /// <returns>The string value representing the key use.</returns>
    public override string ToString() => Value;
    
    
}