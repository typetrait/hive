using MessagePack;

namespace Hive.Core.Network.Packet;

[MessagePackObject]
public class MousePacket : IHivePacket
{
    [Key(0)]
    public int X { get; set; }

    [Key(1)]
    public int Y { get; set; }

    public MousePacket(int x, int y)
    {
        X = x;
        Y = y;
    }
}
