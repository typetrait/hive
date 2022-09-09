using Hive.Core.Network.Packet;
using System.Net;
using System.Net.Sockets;

namespace Hive.Core.Network;

public class HiveServer
{
    public event EventHandler<ClientEventArgs>? ClientConnected;
    public event EventHandler<PacketEventArgs>? PacketReceived;

    public bool IsListening { get; private set; }

    private readonly TcpListener _tcpListener;

    public HiveServer()
    {
        _tcpListener = new TcpListener(IPAddress.Any, 35776);
        IsListening = false;
    }

    public async Task Start()
    {
        _tcpListener.Start();
        IsListening = true;
        await Task.Factory.StartNew(HandleClientConnection);
    }

    public void Stop()
    {
        IsListening = false;
    }

    private async Task HandleClientConnection()
    {
        while (IsListening)
        {
            var _tcpClient = await _tcpListener.AcceptTcpClientAsync();
            var client = new HiveClient(_tcpClient);

            OnClientConnected(client);

            await Task.Factory.StartNew(() =>
            {
                var isConnected = true;

                while (isConnected)
                {
                    var packet = client.ReceivePacket();
                    OnPacketReceived(client, packet);
                }
            });
        }

        _tcpListener.Stop();
    }

    private void OnClientConnected(HiveClient client)
    {
        ClientConnected?.Invoke(this, new ClientEventArgs(client));
    }

    private void OnPacketReceived(HiveClient client, IHivePacket packet)
    {
        PacketReceived?.Invoke(this, new PacketEventArgs(client, packet));
    }
}
