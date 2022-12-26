using MessagePack;

namespace Hive.Core.Network.Packet;

[MessagePackObject]
public class KeyboardButtonDownPacket : IHivePacket
{
    [Key(0)]
    public uint Key { get; set; }

    public KeyboardButtonDownPacket(uint key)
    {
        Key = key;
    }
}
