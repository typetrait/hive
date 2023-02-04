using Hive.Core.Network;

namespace Hive.Core.Client;

public class Controller
{
    public HiveClient Client { get; init; }
    public IEnumerable<Boundary> Boundaries { get; init; }

    private readonly CommandDispatcher _dispatcher;

    public Controller(HiveClient client)
    {
        Client = client;
        _dispatcher = new CommandDispatcher();
    }

    public void Relay(IHivePacket packet)
    {
        var commandContext = new CommandContext(null);
        _dispatcher.Dispatch(packet, commandContext);
    }
}
