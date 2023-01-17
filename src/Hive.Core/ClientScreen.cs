using Hive.Core.Native;

namespace Hive.Core;

public class ClientScreen
{
    public int Width { get; set; }
    public int Height { get; set; }

    public ClientScreen(Screen screen)
    {
        Width = screen.Bounds.Width;
        Height = screen.Bounds.Height;
    }

    public static (int, int) GetDisplayResolution(Screen screen)
    {
        var sf = ScreenNative.GetWindowsScreenScalingFactor(false);
        var screenWidth = screen.Bounds.Width * sf;
        var screenHeight = screen.Bounds.Height * sf;
        return ((int)screenWidth, (int)screenHeight);
    }

    public static ClientScreen FromPrimary()
    {
        return new ClientScreen(Screen.PrimaryScreen);
    }
}
