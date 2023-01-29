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
    public static IInputCommand CreateCommand(IHivePacket packet)
    {
        return packet switch
        {
            MouseButtonDownPacket mouseDownPacket => new MouseButtonInputCommand(mouseDownPacket.X, mouseDownPacket.Y, Input.ButtonState.Down),
            MouseButtonUpPacket mouseUpPacket => new MouseButtonInputCommand(mouseUpPacket.X, mouseUpPacket.Y, Input.ButtonState.Up),
            KeyboardButtonDownPacket keyboardDownPacket => new KeyboardInputCommand(),
            KeyboardButtonUpPacket keyboardUpPacket => new KeyboardInputCommand(),
            MouseMovePacket mouseMovePacket => new MouseInputCommand(mouseMovePacket.X, mouseMovePacket.Y),
            _ => throw new ArgumentException("The provided argument is not convertible into an input command.")
        };
    }
}
