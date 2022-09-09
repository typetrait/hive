using Hive.App.ViewModel;
using Hive.Core.Input;
using System.Windows;

namespace Hive.App.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var keyboardHook = new KeyboardHook();
        var mouseHook = new MouseHook();
        DataContext = new MainViewModel(keyboardHook, mouseHook);
    }
}
