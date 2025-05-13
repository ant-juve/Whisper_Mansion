using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour, Interactable
{
    public string keyId;
    public string keyName;
    public string description;
    public Sprite keyIcon;

    public void Interact()
    {
        InventoryItem item = new InventoryItem(keyId, keyName, description, keyIcon);

        Inventory.AddItem(item);
        KeyIconHUD.Instance.AddIcon(keyId, keyIcon);

        MessagePanelController.Instance.ShowMessage("You picked up " + keyName);
        gameObject.SetActive(false); // hide the key object
    }
}

