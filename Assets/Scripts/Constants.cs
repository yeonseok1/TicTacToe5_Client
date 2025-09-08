
public static class Constants
{
    public const string ServerURL = "http://localhost:3000";
    public const string SocketServerURL = "ws://localhost:3000";

    public enum MultiplayControllerState
    {
        CreateRoom, // �� ����
        JoinRoom,   // ������ �濡 ����
        StartGame,  // ������ �濡 �ٸ� ������ �����ؼ� ���� ����
        ExitRoom,   // Ŭ���̾�Ʈ�� ���� ���������� ��
        EndGame     // ������ ������ ���ų� ���� ������ ��
    }

    public enum GameType { SinglePlay, DualPlay, MultiPlay }
    public enum PlayerType { None, PlayerA, PlayerB }

    public const int blockColumnCount = 3;
}
