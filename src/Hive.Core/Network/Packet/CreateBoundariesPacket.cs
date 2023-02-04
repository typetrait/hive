namespace Hive.Core.Network.Packet;

public class CreateBoundariesPacket : IHivePacket
{
    public IList<Boundary> Boundaries { get; init; }

    public CreateBoundariesPacket(IList<Boundary> boundaries)
    {
        Boundaries = boundaries;
    }
}
