using Hive.Core.Input;

namespace Hive.Core;

public class MouseContext
{
    public IList<Boundary> Boundaries { get; init; }
    public Boundary ActiveBoundary { get; private set; }
    public bool IsServerBoundary => _index == 0;

    public event EventHandler<Input.MouseEventArgs>? BoundaryChanged;

    private int _index;
    private readonly MouseHook _mouseHook;

    public MouseContext(MouseHook mouseHook)
    {
        _mouseHook= mouseHook;
        _mouseHook.Moved += OnMouseMoved;

        Boundaries = new List<Boundary>()
        {
            Boundary.FromPrimary()
        };

        _index = 0;
        ActiveBoundary = Boundaries[_index];
    }

    private void OnMouseMoved(object? sender, Input.MouseEventArgs e)
    {
        if (e.X >= ActiveBoundary.Width)
        {
            ActiveBoundary = Boundaries[_index++];
            BoundaryChanged?.Invoke(this, e);
        }
        else if (e.X <= 0)
        {
            ActiveBoundary= Boundaries[_index--];
            BoundaryChanged?.Invoke(this, e);
        }
    }
}
