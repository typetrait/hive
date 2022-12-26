using Hive.Core.Network.Packet;

namespace Hive.Core.Network;

[MessagePack.Union(0, typeof(MouseMovePacket))]
[MessagePack.Union(1, typeof(MouseButtonDownPacket))]
[MessagePack.Union(2, typeof(MouseButtonUpPacket))]
[MessagePack.Union(3, typeof(KeyboardButtonDownPacket))]
[MessagePack.Union(4, typeof(KeyboardButtonUpPacket))]
public interface IHivePacket
{
}
