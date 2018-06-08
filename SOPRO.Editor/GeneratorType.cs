using System;
/// <summary>
/// List of generator types available
/// </summary>
[Serializable]
public enum GeneratorType
{
    /// <summary>
    /// No generator
    /// </summary>
    None = 0,
    /// <summary>
    /// Basic containers generator
    /// </summary>
    Containers,
    /// <summary>
    /// Events generator
    /// </summary>
    Events,
    /// <summary>
    /// Variables generator
    /// </summary>
    Variables,
}