
using System.Diagnostics;

public class GameLogic
{
    public BlockController blockController;         // Block�� ó���� ��ü

    private Constants.PlayerType[,] _board;         // board�� ����

    public BasePlayerState firstPlayerState;
    public BasePlayerState secondPlayerState;

    public enum GameResult {  None, WIn, Lose, Draw }

    private BasePlayerState _currentPlayerState;

    public GameLogic(BlockController _blockController, Constants.GameType gameType)
    {
        blockController = _blockController;

        _board =
            new Constants.PlayerType[Constants.blockColumnCount, Constants.blockColumnCount];

        switch (gameType)
        {
            case Constants.GameType.SinglePlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new AIState();
                SetState(firstPlayerState);
                break;
            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);
                SetState(firstPlayerState);
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    public Constants.PlayerType[,] GetBoard()
    {
        return _board;
    }

    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(this);
    }

    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        else if (playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }

        return false;
    }

    // Game Over ó��
    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        GameManager.Instance.OpenConfirmPanel("���ӿ���", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }

    // ������ ��� Ȯ��
    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board)) { return GameResult.WIn; }  // A�¸�
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board)) { return GameResult.Lose; } // A�й�
        if(CheckGameDraw(_board)) { return GameResult.Draw; }                               // ���º�
        return GameResult.None;                                                             // ��� ����
    }

    // ������ Ȯ��
    public static bool CheckGameDraw(Constants.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None) return false;
            }
        }

        return true;
    }

    // ���� �¸� Ȯ��
    public static bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        // col üũ
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType &&
                board[row, 1] == playerType &&
                board[row, 2] == playerType)
            {
                return true;
            }
        }
        // row üũ
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType &&
                board[1, col] == playerType &&
                board[2, col] == playerType)
            {
                return true;
            }
        }
        // �밢�� üũ
        if (board[0, 0] == playerType &&
            board[1, 1] == playerType &&
            board[2, 2] == playerType)
        {
            return true;
        }
        if (board[0, 2] == playerType &&
            board[1, 1] == playerType &&
            board[2, 0] == playerType)
        {
            return true;
        }

        // ��𿡵� �ɸ��� �ʴ´ٸ�
        return false;
    }
}
