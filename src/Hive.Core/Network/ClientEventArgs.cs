namespace Hive.Core.Network;

public class ClientEventArgs : EventArgs
{
    public readonly HiveClient Client;

    public ClientEventArgs(HiveClient client)
    {
        Client = client;
    }
}
