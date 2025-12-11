using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour
{
    [SerializeField] private InputField emailIF;
    [SerializeField] private InputField passwordIF;

    [SerializeField] private PopUp popUp;

    private const string emailRegex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

    [SerializeField] private string testEmail;
    [SerializeField] private string testPassword;

    public void Login()
    {
        if (string.IsNullOrEmpty(emailIF.text))
        {
            popUp.ShowPopUp(Messages.Enter("email"));
            return;
        }

        if (string.IsNullOrEmpty(passwordIF.text))
        {
            popUp.ShowPopUp(Messages.Enter("password"));
            return;
        }

        if (!Regex.IsMatch(emailIF.text, emailRegex))
        {
            popUp.ShowPopUp(Messages.InvalidEmail);
            return;
        }

        if (!emailIF.text.Equals(testEmail))
        {
            popUp.ShowPopUp(Messages.CredentialCheck);
            return;
        }

        if (!passwordIF.text.Equals(testPassword))
        {
            popUp.ShowPopUp(Messages.CredentialCheck);
            return;
        }

        SceneManager.LoadScene(SceneName.HumanoidScene);
    }
}