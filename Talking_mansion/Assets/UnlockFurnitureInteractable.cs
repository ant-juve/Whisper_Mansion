using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFurnitureInteractable : MonoBehaviour, Interactable
{
    public FurnitureAmbushWithKeyReset linkedAmbush;

    public void Interact()
    {
        if (linkedAmbush != null)
        {
            linkedAmbush.Interact(); // This will use the key and unlock the furniture
        }
    }
}

