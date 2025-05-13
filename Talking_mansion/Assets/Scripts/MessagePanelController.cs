using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePanelController : MonoBehaviour
{
    public static MessagePanelController Instance;

    public GameObject panel;
    public TextMeshProUGUI messageText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMessage()
    {
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
