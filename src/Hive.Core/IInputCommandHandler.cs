namespace Hive.Core;

public interface IInputCommandHandler<T> where T : IInputCommand
{
    void Handle(T command);
}
