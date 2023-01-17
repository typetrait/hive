using System.Collections.Generic;

namespace Hive.Core;

public class MouseContext
{
    public bool IsInClientArea => true;

    private readonly ClientScreen _screen;

    public MouseContext(ClientScreen screen)
    {
        _screen = screen;
    }

    public (int x, int y) GetTranslatedPoint(int x, int y)
    {
        return (x, y);
    }
}
