using Hive.Core.Native;

namespace Hive.Core.Input;

public class MouseInputRelay
{
    private readonly MouseContext _mouseContext;

    public MouseInputRelay(MouseContext mouseContext)
    {
        _mouseContext = mouseContext;
        _mouseContext.BoundaryChanged += OnBoundaryChanged;
    }

    public void RelayMousePosition(int x, int y)
    {
        if (!_mouseContext.IsServerBoundary)
        {
            // TODO: Translate cursor position or use delta to accommodate different resolutions.
            MouseNative.SetCursorPos(x, y);
        }
    }

    private void OnBoundaryChanged(object? sender, MouseEventArgs e)
    {
    }
}
