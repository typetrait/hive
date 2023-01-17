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
        var (scaledX, scaledY) = _mouseContext.GetTranslatedPoint(x, y);

        if (_mouseContext.IsInClientArea)
        {
            MouseNative.SetCursorPos(scaledX, scaledY);
        }
    }
}
