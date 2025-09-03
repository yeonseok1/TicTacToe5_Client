
public class PlayerState : BasePlayerState
{
    private bool _isFirstPlayer;
    private Constants.PlayerType _playerType;

    public PlayerState(bool isFirstPlayer)
    {
        _isFirstPlayer = isFirstPlayer;
        _playerType = _isFirstPlayer?
            Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
    }

    #region �ʼ� �޼���
    public override void OnEnter(GameLogic gameLogic)
    {
        // 1. First Player���� Ȯ���ؼ� UI�� ���� �� ǥ��
        // TODO: Game ���� �� ǥ�� UI ���� ��

        // 2. Block Controller���� �ؾ� �� �� ����
        gameLogic.blockController.OnBlockClickedDelegate = (row, col) =>
        {
            HandleMove(gameLogic, row, col);
        };
    }

    public override void OnExit(GameLogic gameLogic)
    {
        gameLogic.blockController.OnBlockClickedDelegate = null;
    }

    public override void HandleMove(GameLogic gameLogic, int row, int col)
    {
        ProcessMove(gameLogic, _playerType, row, col);
    }


    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (_isFirstPlayer)
        {
            // TODO: Second Player Ȱ��ȭ ��û
        }
        else
        {
            // TODO: First Player Ȱ��ȭ ��û
        }
    }
    #endregion
}
