using MessagePack;

namespace Hive.Core.Network.Packet;

[MessagePackObject]
public class KeyboardButtonUpPacket : IHivePacket
{
    [Key(0)]
    public uint Key { get; set; }

    public KeyboardButtonUpPacket(uint key)
    {
        Key = key;
    }
}
