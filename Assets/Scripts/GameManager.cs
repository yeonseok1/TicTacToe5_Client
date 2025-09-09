using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;   // Ȯ�� �г�
    [SerializeField] private GameObject signinPanel;    // �α��� �г�
    [SerializeField] private GameObject signupPanel;    // ȸ������ �г�

    private Constants.GameType _gameType;

    private Canvas _canvas;

    private GameLogic _gameLogic;

    private GameUIController _gameUIController;

    void Start()
    {
        var sid = PlayerPrefs.GetString("sid");
        if (string.IsNullOrEmpty(sid))
        {
            OpenSigninPanel();
        }
    }

    /// <summary>
    /// Main���� Game Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>
    public void ChangeToGameSence(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }
    // Game���� Main Scene���� ��ȯ�� ȣ��� �޼���
    public void ChangeToMainScene()
    {
        _gameLogic?.Dispose();
        _gameLogic = null;

        SceneManager.LoadScene("Main");
    }

    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }

    public void OpenSigninPanel()
    {
        if(_canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
            signinPanelObject.GetComponent<SigninPanelController>().Show();
        }
    }

    public void OpenSignupPanel()
    {
        if (_canvas != null)
        {
            var signupPanelObject = Instantiate(signupPanel, _canvas.transform);
            signupPanelObject.GetComponent<SignupPanelController>().Show();
        }
    }

    // Game Scene���� ���� ǥ���ϴ� UI ����
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block �ʱ�ȭ
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }

            // Game UI Controller �Ҵ� �� �ʱ�ȭ
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic ����
            if (_gameLogic != null) _gameLogic.Dispose();
            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }

    private void OnApplicationQuit()
    {
        _gameLogic?.Dispose();
        _gameLogic = null;
    }
}