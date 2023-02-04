using Hive.Core.Native;

namespace Hive.Core.Client.Commands;

public class MouseInputCommand : NotifiableInputCommand
{
    private readonly int _x;
    private readonly int _y;

    public MouseInputCommand(CommandContext commandContext, int x, int y) : base(commandContext)
    {
        _x = x;
        _y = y;
    }

    public override void Execute()
    {
        SetCursorPosTransformed(_x, _y);
        base.Execute();
    }

    protected void SetCursorPosTransformed(int x, int y)
    {
        MouseNative.SetCursorPos(_x, _y);
    }
}
