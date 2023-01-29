using Hive.App.Command;
using Hive.Core;
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

    private readonly MouseContext _mouseContext;
    private readonly MouseInputRelay _mouseInputRelay;

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

        _keyboardHook.KeyPressed += (sender, args) => { HiveClient?.SendPacket(new KeyboardButtonDownPacket(args.Key)); };
        _keyboardHook.KeyReleased += (sender, args) => { HiveClient?.SendPacket(new KeyboardButtonUpPacket(args.Key)); };

        _mouseHook.ButtonPressed += (sender, args) => { HiveClient?.SendPacket(new MouseButtonDownPacket(args.X, args.Y)); };
        _mouseHook.ButtonReleased += (sender, args) => { HiveClient?.SendPacket(new MouseButtonUpPacket(args.X, args.Y)); };

        _mouseHook.Moved += (sender, args) => { HiveClient?.SendPacket(new MouseMovePacket(args.X, args.Y)); };

        _mouseContext = new MouseContext(_mouseHook);
        _mouseInputRelay = new MouseInputRelay(_mouseContext);
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

        // TODO: Only send CreateBoundaryPacket when not running both the server and client locally.
        // HiveClient?.SendPacket(new CreateBoundaryPacket(Boundary.FromPrimary()));
    }

    private void OnPacketReceived(object? sender, PacketEventArgs e)
    {
        var packet = e.Packet;

        Log = packet switch
        {
            MouseButtonDownPacket mouseDownPacket => $"Mouse Button Down Packet Received: X = {mouseDownPacket.X}, Y = {mouseDownPacket.Y}.",
            MouseButtonUpPacket mouseUpPacket => $"Mouse Button Up Packet Received: X = {mouseUpPacket.X}, Y = {mouseUpPacket.Y}.",
            KeyboardButtonDownPacket keyboardDownPacket => $"Keyboard Button Down Packet Received: Keycode = {keyboardDownPacket.Key}.",
            KeyboardButtonUpPacket keyboardUpPacket => $"Keyboard Button Up Packet Received: Keycode = {keyboardUpPacket.Key}.",
            MouseMovePacket mouseMovePacket => $"Mouse Moved Packet Received: X = {mouseMovePacket.X}, Y = {mouseMovePacket.Y}.",
            _ => throw new System.NotImplementedException()
        };

        if (packet is MouseMovePacket mp)
        {
            OnMouseMoved(mp);
        }
        else if (packet is CreateBoundaryPacket bp)
        {
            OnCreateBoundary(bp);
        }
    }

    private void OnMouseMoved(MouseMovePacket mouseMovePacket)
    {
        _mouseInputRelay.RelayMousePosition(mouseMovePacket.X, mouseMovePacket.Y);
    }

    private void OnCreateBoundary(CreateBoundaryPacket createBoundaryPacket)
    {
        _mouseContext.Boundaries.Add(createBoundaryPacket.Boundary);
    }
}
