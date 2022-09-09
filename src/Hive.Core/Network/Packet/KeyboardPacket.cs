using MessagePack;

namespace Hive.Core.Network.Packet;

[MessagePackObject]
public class KeyboardPacket : IHivePacket
{
    [Key(0)]
    public uint Key { get; set; }

    public KeyboardPacket(uint key)
    {
        Key = key;
    }
}
