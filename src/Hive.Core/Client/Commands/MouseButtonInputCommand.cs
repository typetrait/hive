namespace Hive.Core.Client.Commands;

public class MouseButtonInputCommand : MouseInputCommand
{
    private readonly Input.ButtonState _buttonState;

    public MouseButtonInputCommand(CommandContext commandContext, int x, int y, Input.ButtonState buttonState) : base(commandContext, x, y)
    {
        _buttonState = buttonState;
    }

    public override void Execute()
    {
        switch (_buttonState)
        {
            case Input.ButtonState.Up:
                break;

            case Input.ButtonState.Down:
                break;
        }

        base.Execute();
    }
}
