using Hive.Core.Native;

namespace Hive.Core;

/// <summary>
/// Represents an area where input commands can be executed.
/// Effectively 
/// </summary>
public class Boundary
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Boundary(Screen screen)
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

    public static Boundary FromPrimary()
    {
        return new Boundary(Screen.PrimaryScreen);
    }

    public static IEnumerable<Boundary> GetAll()
    {
        foreach (var screen in Screen.AllScreens)
        {
            yield return new Boundary(screen);
        }
    }
}
