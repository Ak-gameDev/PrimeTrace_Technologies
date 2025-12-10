using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour
{
    public void Login()
    {
        SceneManager.LoadScene(SceneName.HumanoidScene);
    }
}