using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FurnitureBlocker : MonoBehaviour, Interactable
{
    public string requiredKey = "ClearPathKey";
    public string messageIfLocked = "A wall of furniture blocks your path.";
    public string messageIfUnlocked = "The furniture rearranges itself...";

    [Header("Furniture Setup")]
    public Transform[] furniturePieces;
    public Transform[] finalPositions; // Empty objects with final position & rotation

    public float moveSpeed = 1.5f;
    private bool cleared = false;

    public void Interact()
    {
        if (cleared)
        {
            MessagePanelController.Instance.ShowMessage("The path is already clear.");
            return;
        }

        if (Inventory.HasItem(requiredKey))
        {
            MessagePanelController.Instance.ShowMessage(messageIfUnlocked);
            Inventory.RemoveItem(requiredKey);
            KeyIconHUD.Instance.RemoveIcon(requiredKey);

            for (int i = 0; i < furniturePieces.Length; i++)
            {
                if (i < finalPositions.Length)
                {
                    StartCoroutine(MoveFurniture(furniturePieces[i], finalPositions[i]));
                }
            }

            cleared = true;
        }
        else
        {
            MessagePanelController.Instance.ShowMessage(messageIfLocked);
        }
    }

    IEnumerator MoveFurniture(Transform furniture, Transform target)
    {
        Vector3 startPos = furniture.position;
        Quaternion startRot = furniture.rotation;

        Vector3 endPos = target.position;
        Quaternion endRot = target.rotation;

        float elapsed = 0f;
        float duration = 1f / moveSpeed;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            furniture.position = Vector3.Lerp(startPos, endPos, t);
            furniture.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        furniture.position = endPos;
        furniture.rotation = endRot;
    }
}
