using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private Text messageTxt;
    [SerializeField] private Button actionBtn;

    private void Start()
    {
        HidePopUp();
    }

    public void ShowPopUp(string message, Action action = null)
    {
        actionBtn.onClick.RemoveAllListeners();

        actionBtn.onClick.AddListener(() =>
        {
            action?.Invoke();
            HidePopUp();
        });

        messageTxt.text = message;

        gameObject.SetActive(true);
    }

    public void HidePopUp()
    {
        gameObject.SetActive(false);
    }
}