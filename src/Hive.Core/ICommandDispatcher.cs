using Hive.Core.Network;

namespace Hive.Core;

public interface ICommandDispatcher
{
    void Dispatch<T>(T packet, CommandContext context) where T : IHivePacket;
}
