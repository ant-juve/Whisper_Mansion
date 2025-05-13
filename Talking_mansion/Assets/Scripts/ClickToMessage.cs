using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMessage : MonoBehaviour, Interactable
{
    [TextArea(2, 5)]
    public string[] messages;
    private int currentMessageIndex = 0;

    public void Interact()
    {
        if (messages.Length == 0) return;

        MessagePanelController.Instance.ShowMessage(messages[currentMessageIndex]);

        if (currentMessageIndex < messages.Length - 1)
        {
            currentMessageIndex++;
        }
    }
}



