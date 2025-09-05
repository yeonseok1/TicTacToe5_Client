using TMPro;
using UnityEngine;

public struct SigninData
{
    public string username;
    public string password;
}

public struct SigninResult
{
    public int result;
}

public class SigninPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;


    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            // TODO: ������ ���� �Է��ϵ��� ��û
            Shake();
            return;
        }

        var signinData = new SigninData();
        signinData.username = username;
        signinData.password = password;

        // Signin �Լ��� Username/Password ����
        StartCoroutine(NetworkManager.Instance.Signin(signinData,
            () =>
            {
                Hide();
            },
            (result) =>
            {
                if (result == 0)
                {
                    GameManager.Instance.OpenConfirmPanel("���������� ��ȿ���� �ʽ��ϴ�.",
                        () =>
                        {
                            usernameInputField.text = "";
                            passwordInputField.text = "";
                        });
                }
                else if (result == 1)
                {
                    GameManager.Instance.OpenConfirmPanel("�н����尡 ��ȿ���� �ʽ��ϴ�.",
                        () =>
                        {
                            passwordInputField.text = "";
                        });
                }
            }));


        
    }

    public void OnClickCloseButton()
    {
        Hide();
    }

    public void OnClickSignupButton()
    {

    }
}
