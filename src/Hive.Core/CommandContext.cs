namespace Hive.Core;

public class CommandContext
{
    public Boundary ActiveBoundary { get; init; }

    public CommandContext(Boundary activeBoundary)
    {
        ActiveBoundary = activeBoundary;
    }
}
