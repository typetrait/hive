using Hive.Core.Native;
using System.Runtime.InteropServices;

namespace Hive.Core.Input;

public class KeyboardHook
{
    public event EventHandler<KeyboardEventArgs>? KeyPressed;
    public event EventHandler<KeyboardEventArgs>? KeyReleased;

    private const int WM_KEYUP = 0x0101;
    private const int WM_KEYDOWN = 0x0100;

    private readonly IntPtr _id;
    private readonly WindowsHookNative.HookProc _keyboardCallback;
    private readonly GCHandle _gcHandle;

    public KeyboardHook()
    {
        _keyboardCallback = OnGlobalKeyboardEvent;
        _gcHandle = GCHandle.Alloc(_keyboardCallback);
        _id = WindowsHookNative.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, _keyboardCallback, IntPtr.Zero, 0);
    }

    private IntPtr OnGlobalKeyboardEvent(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode < 0) return WindowsHookNative.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

        var message = (int)wParam;
        var eventData = Marshal.PtrToStructure<KeyboardHookData>(lParam);

        if (message == WM_KEYDOWN)
        {
            var args = new KeyboardEventArgs(eventData.vkCode);
            KeyPressed?.Invoke(this, args);
            
        }
        else if (message == WM_KEYUP)
        {
            var args = new KeyboardEventArgs(eventData.vkCode);
            KeyReleased?.Invoke(this, args);
        }

        return WindowsHookNative.CallNextHookEx(_id, nCode, wParam, lParam);
    }
}
