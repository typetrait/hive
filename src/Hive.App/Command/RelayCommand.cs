using System;
using System.Windows.Input;

namespace Hive.App.Command;

public class RelayCommand<T> : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    private readonly Action<T> _execute;
    private readonly Predicate<T>? _canExecute;

    public RelayCommand(Action<T> execute) : this(execute, null)
    {
    }

    public RelayCommand(Action<T> execute, Predicate<T>? canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException("execute");
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute((T)parameter!);

    public void Execute(object? parameter) => _execute((T)parameter!);
}
