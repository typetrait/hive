using MessagePack;
using MessagePack.Resolvers;
using System.Net.Sockets;

namespace Hive.Core.Network;

public class HiveClient
{
    public readonly TcpClient TcpClient;
    private readonly MessagePackSerializerOptions _options;

    public HiveClient(TcpClient tcpClient)
    {
        TcpClient = tcpClient;

        _options = MessagePackSerializerOptions.Standard.WithResolver(StandardResolver.Instance);
    }

    public void SendPacket(IHivePacket packet)
    {
        var packetBytes = MessagePackSerializer.Serialize(packet);
        var packetSize = packetBytes.Length;

        var stream = TcpClient.GetStream();

        stream.Write(BitConverter.GetBytes(packetSize), 0, 4);
        stream.Write(packetBytes, 0, packetSize);
    }

    public IHivePacket ReceivePacket()
    {
        var packetBytes = ReadData();
        var packet = MessagePackSerializer.Deserialize<IHivePacket>(packetBytes);

        return packet;
    }

    public byte[] ReadData()
    {
        var stream = TcpClient.GetStream();

        var packetLengthBytes = new byte[4];

        var headerBytesRead = 0;
        while (headerBytesRead < packetLengthBytes.Length)
        {
            var read = stream.Read(packetLengthBytes, headerBytesRead, packetLengthBytes.Length);
            headerBytesRead += read;
        }

        var packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

        var packetData = new byte[packetLength];

        var bytesRead = 0;
        while (bytesRead < packetLength)
        {
            var read = stream.Read(packetData, bytesRead, packetLength - bytesRead);
            bytesRead += read;
        }

        return packetData;
    }
}
