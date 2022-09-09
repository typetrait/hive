using Hive.App.Command;
using Hive.Core.Input;
using Hive.Core.Network;
using Hive.Core.Network.Packet;
using System.Windows.Input;

namespace Hive.App.ViewModel;

public class MainViewModel : ViewModelBase
{
    public ICommand StartServerCommand { get; init; }
    public ICommand ConnectCommand { get; init; }
    public HiveServer HiveServer { get; init; }
    public HiveClient? HiveClient { get; private set; }

    private readonly KeyboardHook _keyboardHook;
    private readonly MouseHook _mouseHook;

    private string _address;
    public string Address
    {
        get { return _address; }
        set
        {
            _address = value;
            OnPropertyChanged(nameof(Address));
        }
    }

    private string? _log;
    public string? Log
    {
        get { return _log; }
        set
        {
            _log = value;
            OnPropertyChanged(nameof(Log));
        }
    }

    public MainViewModel(KeyboardHook keyboardHook, MouseHook mouseHook)
    {
        HiveServer = new HiveServer();
        StartServerCommand = new RelayCommand<bool>(StartServer);
        ConnectCommand = new RelayCommand<bool>(Connect);

        _address = string.Empty;

        Log = string.Empty;
        _keyboardHook = keyboardHook;
        _mouseHook = mouseHook;

        _keyboardHook.KeyPressed += (sender, args) => { HiveClient?.SendPacket(new KeyboardPacket(args.Key)); };
        // _keyboardHook.KeyReleased += (sender, args) => { Log = string.Empty; };

        _mouseHook.ButtonPressed += (sender, args) => { HiveClient?.SendPacket(new MousePacket(args.X, args.Y)); };
        // _mouseHook.ButtonReleased += (sender, args) => { Log = string.Empty; };
    }

    private async void StartServer(bool status)
    {
        await HiveServer.Start();

        HiveServer.ClientConnected += (sender, args) => Log += $"\nClient Connected!";
        HiveServer.PacketReceived += OnPacketReceived;
    }

    private void Connect(bool status)
    {
        var tcpClient = new System.Net.Sockets.TcpClient(Address, 35776);
        HiveClient ??= new HiveClient(tcpClient);
    }

    private void OnPacketReceived(object? sender, PacketEventArgs e)
    {
        var packet = e.Packet;

        if (packet is MousePacket mousePacket)
        {
            Log = $"Mouse Packet Received: X = {mousePacket.X}, Y = {mousePacket.Y}.";
        }
        else if (packet is KeyboardPacket keyboardPacket)
        {
            Log = $"Keyboard Packet Received: Keycode = {keyboardPacket.Key}.";
        }
    }
}
