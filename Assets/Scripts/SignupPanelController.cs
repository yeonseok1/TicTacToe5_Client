using TMPro;
using UnityEngine;

public struct SignupData
{
    public string username;
    public string password;
    public string nickname;
}

public struct SignupResult
{
    public int result;
}

public class SignupPanelController : PanelController
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    [SerializeField] private TMP_InputField nicknameInputField;

    public void OnClickConfirmButton()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        string nickname = nicknameInputField.text;


        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            // TODO: ������ ���� �Է��ϵ��� ��û
            Shake();
            return;
        }

        // Confirm Password Ȯ��
        if (password.Equals(confirmPassword))
        {
            var signupData = new SignupData();
            signupData.username = username;
            signupData.password = password;
            signupData.nickname = nickname;


            // Signin �Լ��� Username/Password ����
            StartCoroutine(NetworkManager.Instance.Signup(signupData,
                () =>
                {
                    GameManager.Instance.OpenConfirmPanel("ȸ�����Կ� �����߽��ϴ�.",
                        () =>
                        {
                            Hide();
                        });
                },
                (result) =>
                {
                    if (result == 0)
                    {
                        GameManager.Instance.OpenConfirmPanel("�̹� �����ϴ� ������Դϴ�.",
                            () =>
                            {
                                usernameInputField.text = "";
                                passwordInputField.text = "";
                                confirmPasswordInputField.text = "";
                                nicknameInputField.text = "";
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
    }

    public void OnClickCancelButton()
    {
        Hide();
    }

}
