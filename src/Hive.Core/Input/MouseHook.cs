using Hive.Core.Native;
using System.Runtime.InteropServices;

namespace Hive.Core.Input;

public class MouseHook
{
    public event EventHandler<MouseEventArgs>? ButtonPressed;
    public event EventHandler<MouseEventArgs>? ButtonReleased;

    private const int WM_LBUTTONDOWN = 0x0201;
    private const int WM_RBUTTONDOWN = 0x0204;
    private const int WM_LBUTTONUP = 0x0202;
    private const int WM_RBUTTONUP = 0x0205;

    private readonly IntPtr _id;
    private readonly WindowsHookNative.HookProc _mouseCallback;
    private readonly GCHandle _gcHandle;

    public MouseHook()
    {
        _mouseCallback = OnGlobalMouseEvent;
        _gcHandle = GCHandle.Alloc(_mouseCallback);
        _id = WindowsHookNative.SetWindowsHookEx(HookType.WH_MOUSE_LL, _mouseCallback, IntPtr.Zero, 0);
    }

    private IntPtr OnGlobalMouseEvent(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode < 0) return WindowsHookNative.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

        var message = (int)wParam;
        var eventData = Marshal.PtrToStructure<MouseHookData>(lParam)!;

        if (message == WM_LBUTTONDOWN || message == WM_RBUTTONDOWN)
        {
            var args = new MouseEventArgs(eventData.pt.X, eventData.pt.Y);
            ButtonPressed?.Invoke(this, args);
        }
        else if (message == WM_LBUTTONUP || message == WM_RBUTTONUP)
        {
            var args = new MouseEventArgs(eventData.pt.X, eventData.pt.Y);
            ButtonReleased?.Invoke(this, args);
        }

        return WindowsHookNative.CallNextHookEx(_id, nCode, wParam, lParam);
    }
}
