namespace Hive.Core.Network.Packet;

public class PacketEventArgs : EventArgs
{
    public readonly HiveClient Client;
    public readonly IHivePacket Packet;

    public PacketEventArgs(HiveClient client, IHivePacket packet)
    {
        Client = client;
        Packet = packet;
    }
}
