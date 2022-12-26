using Hive.Core.Native;

namespace Hive.Core;

public class ScaledResolution
{
    public int Width { get; set; }
    public int Height { get; set; }

    public ScaledResolution(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static ScaledResolution FromActiveScreen()
    {
        var resolution = ResolutionNative.GetDisplayResolution();
        return new ScaledResolution(resolution.Width, resolution.Height);
    }
}
