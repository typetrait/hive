using Hive.Core.Native;

namespace Hive.Core.Client.Commands;

public class MouseInputCommand : IInputCommand
{
    private readonly int _x;
    private readonly int _y;

    public MouseInputCommand(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public virtual void Execute()
    {
        MouseNative.SetCursorPos(_x, _y);
    }
}
