using Hive.Core.Native;

namespace Hive.Core.Input;

public class MouseInputRelay
{
    private readonly MouseContext _mouseContext;

    public MouseInputRelay(MouseContext mouseContext)
    {
        _mouseContext = mouseContext;
    }

    public void RelayMousePosition(int x, int y)
    {
        var (scaledX, scaledY) = _mouseContext.GetScaledPoint(x, y);
        MouseNative.SetCursorPos(scaledX, scaledY);
    }
}
