using Hive.Core.Client;
using Hive.Core.Network;

namespace Hive.Core;

public class CommandDispatcher : ICommandDispatcher
{
    /// <summary>
    /// Dispatch a command for execution based on the corresponding packet and context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="packet"></param>
    /// <param name="context"></param>
    public void Dispatch<T>(T packet, CommandContext commandContext) where T : IHivePacket
    {
        IInputCommand command = InputCommandFactory.CreateCommand(packet, commandContext);
        command.Execute();
    }
}
