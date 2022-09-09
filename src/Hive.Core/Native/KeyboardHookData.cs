using System.Runtime.InteropServices;

namespace Hive.Core.Native;

[StructLayout(LayoutKind.Sequential)]
public struct KeyboardHookData
{
    public uint vkCode;
    public uint scanCode;
    public uint flags;
    public uint time;
    public UIntPtr dwExtraInfo;
}
