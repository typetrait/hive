using Hive.Core.Client.Commands;
using Hive.Core.Network;
using Hive.Core.Network.Packet;

namespace Hive.Core.Client;

/// <summary>
/// Provides an interface for creating <see cref="IInputCommand"/> objects.
/// </summary>
public static class InputCommandFactory
{
    /// <summary>
    /// Creates an <see cref="IInputCommand"/> from an <see cref="IHivePacket"/>.
    /// </summary>
    public static IInputCommand CreateCommand(IHivePacket packet, CommandContext commandContext)
    {
        return packet switch
        {
            MouseButtonDownPacket mouseDownPacket => new MouseButtonInputCommand(commandContext, mouseDownPacket.X, mouseDownPacket.Y, Input.ButtonState.Down),
            MouseButtonUpPacket mouseUpPacket => new MouseButtonInputCommand(commandContext, mouseUpPacket.X, mouseUpPacket.Y, Input.ButtonState.Up),
            KeyboardButtonDownPacket keyboardDownPacket => new KeyboardInputCommand(commandContext),
            KeyboardButtonUpPacket keyboardUpPacket => new KeyboardInputCommand(commandContext),
            MouseMovePacket mouseMovePacket => new MouseInputCommand(commandContext, mouseMovePacket.X, mouseMovePacket.Y),
            _ => throw new ArgumentException("The provided argument is not convertible into an input command.")
        };
    }
}
