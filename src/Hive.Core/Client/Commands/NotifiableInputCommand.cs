namespace Hive.Core.Client.Commands;

public class NotifiableInputCommand : IInputCommand
{
    /// <summary>
    /// Raised when the input is executed.
    /// </summary>
    public event EventHandler? Executed;

    /// <summary>
    /// Contextual information provided for executing commands.
    /// </summary>
    public CommandContext Context { get; init; }

    public NotifiableInputCommand(CommandContext commandContext)
    {
        Context = commandContext;
    }

    public virtual void Execute()
    {
        OnExecute();
    }

    protected void OnExecute()
    {
        Executed?.Invoke(this, EventArgs.Empty);
    }
}
