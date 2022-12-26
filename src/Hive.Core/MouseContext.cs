namespace Hive.Core;

public class MouseContext
{
    public ScaledResolution Resolution { get; set; }
    public bool IsActive { get; set; }

    public MouseContext(ScaledResolution resolution, bool isActive)
    {
        Resolution = resolution;
        IsActive = isActive;
    }

    public (int x, int y) GetScaledPoint(int x, int y)
    {
        return (x, y);
    }
}
