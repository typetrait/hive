using System.Runtime.InteropServices;

namespace Hive.Core.Native
{
    internal static class MouseNative
    {
        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        internal static extern bool SetCursorPos(int x, int y);
    }
}
