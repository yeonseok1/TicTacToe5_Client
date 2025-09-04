
using System.Diagnostics;

public class GameLogic
{
    public BlockController blockController;         // Block을 처리할 객체

    private Constants.PlayerType[,] _board;         // board의 정보

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

    // Game Over 처리
    public void EndGame(GameResult gameResult)
    {
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        GameManager.Instance.OpenConfirmPanel("게임오버", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }

    // 게임의 결과 확인
    public GameResult CheckGameResult()
    {
        if (CheckGameWin(Constants.PlayerType.PlayerA, _board)) { return GameResult.WIn; }  // A승리
        if (CheckGameWin(Constants.PlayerType.PlayerB, _board)) { return GameResult.Lose; } // A패배
        if(CheckGameDraw(_board)) { return GameResult.Draw; }                               // 무승부
        return GameResult.None;                                                             // 계속 진행
    }

    // 비겼는지 확인
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

    // 게임 승리 확인
    public static bool CheckGameWin(Constants.PlayerType playerType, Constants.PlayerType[,] board)
    {
        // col 체크
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType &&
                board[row, 1] == playerType &&
                board[row, 2] == playerType)
            {
                return true;
            }
        }
        // row 체크
        for (var col = 0; col < board.GetLength(1); col++)
        {
            if (board[0, col] == playerType &&
                board[1, col] == playerType &&
                board[2, col] == playerType)
            {
                return true;
            }
        }
        // 대각선 체크
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

        // 어디에도 걸리지 않는다면
        return false;
    }
}
