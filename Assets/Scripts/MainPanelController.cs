using UnityEngine;

public class MainPanelController : MonoBehaviour
{
    public void OnClickSinglePlayButton()
    {
        GameManager.Instance.ChangeToGameSence(Constants.GameType.SinglePlay);
    }

    public void OnClickDualPlayButton()
    {
        GameManager.Instance.ChangeToGameSence(Constants.GameType.DualPlay);
    }

    public void OnClickMultiPlayButton()
    {
        GameManager.Instance.ChangeToGameSence(Constants.GameType.MultiPlay);
    }

    public void OnClickSettingsButton()
    {

    }
}
