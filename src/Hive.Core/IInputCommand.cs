namespace Hive.Core;

/// <summary>
/// Represents an input executed by the client.
/// </summary>
public interface IInputCommand
{
    /// <summary>
    /// Executes the input.
    /// </summary>
    public void Execute();
}
