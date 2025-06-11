using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[Serializable]
public class UI_InputField
{
    public TextMeshProUGUI ResultText;
    public TMP_InputField EmailInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordConfirmInputField;
    public Button ConfirmButton;
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject ResisterPanel;
    [Header("로그인")]
    public UI_InputField LoginInputField;
    [Header("회원가입")]
    public UI_InputField SignUpInputField;

    private void Start()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);

        // LoginCheck();
    }

    public void OnClickGoToResister()
    {
        LoginPanel.SetActive(false);
        ResisterPanel.SetActive(true);
    }

    public void OnClickGoToLogin()
    {
        LoginPanel.SetActive(true);
        ResisterPanel.SetActive(false);
    }


    // 회원가입
    public void Resister()
    {
        SignUpInputField.ResultText.text = "";

        // 1. 이메일 도메인 규칙을 확인한다.
        string email = SignUpInputField.EmailInputField.text;
        AccountEmailSpecification emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsStatisfiedBy(email))
        {
            SignUpInputField.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }
        // 2. 닉네임 도메인 규칙을 확인한다.
        string nickname = SignUpInputField.NicknameInputField.text;
        AccountNicknameSpecification nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsStatisfiedBy(nickname))
        {
            SignUpInputField.ResultText.text = nicknameSpecification.ErrorMessage;
            return;
        }

        // 3. 비밀번호 도메인 규칙을 확인한다.
        string pw = SignUpInputField.PasswordInputField.text;
        AccountPasswordpecification passwordSpecification = new AccountPasswordpecification();
        if (!passwordSpecification.IsStatisfiedBy(pw))
        {
            SignUpInputField.ResultText.text = passwordSpecification.ErrorMessage;
            return;
        }

        string pwConfirm = SignUpInputField.PasswordConfirmInputField.text;
        if (string.IsNullOrEmpty(pw) || string.IsNullOrEmpty(pwConfirm))
        {
            GameObject nullobj = string.IsNullOrEmpty(pw) ? SignUpInputField.PasswordInputField.gameObject : SignUpInputField.PasswordInputField.gameObject;
            SignUpInputField.ResultText.text = "비밀번호와 확인을 입력해주세요.";
            nullobj.transform.DOShakePosition(
                1f, // 지속 시간
                3f, // 흔들림 강도 (Vector3도 가능)
                40 // 점점 줄어들며 종료
            );
            return;
        }
        Result result = AccountManager.Instance.TryRegister(email, nickname, pw);
        if (result.IsSuccess)
        {
            OnClickGoToLogin();
        }
        else
        {
            SignUpInputField.ResultText.text = result.Message;
        }
    }

    public void Login()
    {
        LoginInputField.ResultText.text = "";
        // 1. 아이디 입력을 확인한다.
        string email = LoginInputField.EmailInputField.text;
        AccountEmailSpecification emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsStatisfiedBy(email))
        {
            LoginInputField.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }
        // 2. 비밀번호 입력을 확인한다.
        string password = LoginInputField.PasswordInputField.text;
        if (string.IsNullOrEmpty(password))
        {
            LoginInputField.ResultText.text = "비밀번호는 비어있을 수 없습니다";
        }
        // 3. 맞다면 로그인
        if (AccountManager.Instance.TryLogin(email, password))
        {
            gameObject.SetActive(false);
            Debug.Log("로그인 성공!");
            SceneManager.LoadScene(1);
        }
    }

    public void LoginCheck()
    {
        string email = LoginInputField.EmailInputField.text;
        string password = LoginInputField.PasswordInputField.text;

        LoginInputField.ConfirmButton.enabled = !(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password));
    }
}
