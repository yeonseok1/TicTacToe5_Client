
public class PlayerState : BasePlayerState
{
    private bool _isFirstPlayer;
    private Constants.PlayerType _playerType;

    private MultiplayController _multiplayController;
    private string _roomId;
    private bool _isMultiplay;

    public PlayerState(bool isFirstPlayer)
    {
        _isFirstPlayer = isFirstPlayer;
        _playerType = _isFirstPlayer?
            Constants.PlayerType.PlayerA : Constants.PlayerType.PlayerB;
        _isMultiplay = false;
    }

    public PlayerState(bool isFirstPlayer, MultiplayController multiplayController, string roomId)
        : this(isFirstPlayer)
    {
        _multiplayController = multiplayController;
        _roomId = roomId;
        _isMultiplay = true;
    }



    #region �ʼ� �޼���
    public override void OnEnter(GameLogic gameLogic)
    {
        // 1. First Player���� Ȯ���ؼ� UI�� ���� �� ǥ��
        if (_isFirstPlayer)
        {
            GameManager.Instance.SetGameTurnPanel(GameUIController.GameTurnPanelType.ATurn);
        }
        else
        {
            GameManager.Instance.SetGameTurnPanel(GameUIController.GameTurnPanelType.BTurn);
        }

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

        if (_isMultiplay)
        {
            _multiplayController.DoPlayer(_roomId, row * Constants.blockColumnCount + col);
        }
    }


    protected override void HandleNextTurn(GameLogic gameLogic)
    {
        if (_isFirstPlayer)
        {
            gameLogic.SetState(gameLogic.secondPlayerState);
        }
        else
        {
            gameLogic.SetState(gameLogic.firstPlayerState);
        }
    }
    #endregion
}
