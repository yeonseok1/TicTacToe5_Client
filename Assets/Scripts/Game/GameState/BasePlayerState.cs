
public abstract class BasePlayerState
{
    public abstract void OnEnter(GameLogic gameLogic);         // ���� ����
    public abstract void OnExit(GameLogic gameLogic);          // ���� ����
    public abstract void HandleMove(GameLogic gameLogic, int row, int col);      // ��Ŀ ǥ��
    protected abstract void HandleNextTurn(GameLogic gameLogic);   // �� ��ȯ

    // ���� ��� ó��
    protected void ProcessMove(GameLogic gameLogic, Constants.PlayerType playerType, int row, int col)
    {
        if (gameLogic.SetNewBoardValue(playerType, row, col))
        {
            var gameResult = gameLogic.CheckGameResult();
            if (gameResult == GameLogic.GameResult.None)
            {
                HandleNextTurn(gameLogic);
            }
            else
            {
                gameLogic.EndGame(gameResult);
            }
        }
    }
}
