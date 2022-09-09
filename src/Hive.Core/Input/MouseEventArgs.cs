namespace Hive.Core.Input;

public class MouseEventArgs : EventArgs
{
    public int X { get; set; }
    public int Y { get; set; }

    public MouseEventArgs(int x, int y)
    {
        X = x;
        Y = y;
    }
}
