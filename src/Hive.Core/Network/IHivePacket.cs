using Hive.Core.Network.Packet;

namespace Hive.Core.Network;

[MessagePack.Union(0, typeof(MousePacket))]
[MessagePack.Union(1, typeof(KeyboardPacket))]
public interface IHivePacket
{
}
