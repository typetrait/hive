namespace Hive.Core.Network.Packet;

public class CreateBoundaryPacket : IHivePacket
{
    public Boundary Boundary { get; set; }

    public CreateBoundaryPacket(Boundary boundary)
    {
        Boundary = boundary;
    }
}
