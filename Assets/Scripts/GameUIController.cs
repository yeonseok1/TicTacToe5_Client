using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public void OnClickBackButton()
    {
        //GameManager.Instance.ChangeToMainScene();

        GameManager.Instance.OpenConfirmPanel(message: "������ �����Ͻðڽ��ϱ�?");
    }
}
