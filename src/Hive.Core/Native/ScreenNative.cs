using System.Runtime.InteropServices;

namespace Hive.Core.Native;

internal class ScreenNative
{
    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
    public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

    public enum DeviceCap
    {
        VERTRES = 10,
        DESKTOPVERTRES = 117
    }

    public static double GetWindowsScreenScalingFactor(bool percentage = true)
    {
        Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);

        IntPtr DeviceContextHandle = GraphicsObject.GetHdc();
        int LogicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.VERTRES);
        int PhysicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.DESKTOPVERTRES);
        double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);

        if (percentage)
        {
            ScreenScalingFactor *= 100.0;
        }

        GraphicsObject.ReleaseHdc(DeviceContextHandle);
        GraphicsObject.Dispose();

        return ScreenScalingFactor;
    }

    public static IReadOnlyCollection<Screen> GetAllScreens()
    {
        return Screen.AllScreens;
    }
}
