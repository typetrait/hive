using System.Runtime.InteropServices;

namespace Hive.Core.Native;

[StructLayout(LayoutKind.Sequential)]
public class MouseHookData
{
    public Point pt;
    public int mouseData;
    public int flags;
    public int time;
    public UIntPtr dwExtraInfo;
}
