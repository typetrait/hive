namespace Hive.Core.Input;

public class KeyboardEventArgs : EventArgs
{
    public uint Key { get; set; }

    public KeyboardEventArgs(uint key)
    {
        Key = key;
    }
}
