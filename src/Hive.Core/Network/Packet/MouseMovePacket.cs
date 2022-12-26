using MessagePack;

namespace Hive.Core.Network.Packet;

[MessagePackObject]
public class MouseMovePacket : IHivePacket
{
    [Key(0)]
    public int X { get; set; }

    [Key(1)]
    public int Y { get; set; }

    public MouseMovePacket(int x, int y)
    {
        X = x;
        Y = y;
    }
}
