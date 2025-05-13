using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureAmbushWithKeyReset : MonoBehaviour, Interactable
{
    [Header("Furniture Setup")]
    public Transform[] furniturePieces;
    public Transform[] ambushPositions;  // Where the furniture goes to block the door
    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;

    [Header("Unlocking")]
    public string requiredKey = "ClearPathKey";
    public string unlockMessage = "The furniture slides back into place...";
    public string lockedMessage = "The furniture blocks your way.";

    [Header("Slide Settings")]
    public float moveSpeed = 1.5f;

    private bool ambushed = false;
    private bool cleared = false;

    void Start()
    {
        // Cache original positions and rotations
        int count = furniturePieces.Length;
        originalPositions = new Vector3[count];
        originalRotations = new Quaternion[count];
        for (int i = 0; i < count; i++)
        {
            originalPositions[i] = furniturePieces[i].position;
            originalRotations[i] = furniturePieces[i].rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!ambushed && other.CompareTag("Player"))
        {
            ambushed = true;
            GetComponent<AudioSource>()?.Play();

            // Slide all pieces into ambush positions
            for (int i = 0; i < furniturePieces.Length; i++)
            {
                if (i < ambushPositions.Length)
                {
                    StartCoroutine(MoveFurniture(
                        furniturePieces[i],
                        ambushPositions[i].position,
                        ambushPositions[i].rotation
                    ));
                }
            }
        }
    }

    public void Interact()
    {
        if (cleared)
        {
            MessagePanelController.Instance.ShowMessage("The path is already clear.");
            return;
        }

        if (!Inventory.HasItem(requiredKey))
        {
            MessagePanelController.Instance.ShowMessage(lockedMessage);
            return;
        }

        MessagePanelController.Instance.ShowMessage(unlockMessage);
        Inventory.RemoveItem(requiredKey);
        KeyIconHUD.Instance.RemoveIcon(requiredKey);

        // Slide everything back to original
        for (int i = 0; i < furniturePieces.Length; i++)
        {
            StartCoroutine(MoveFurniture(
                furniturePieces[i],
                originalPositions[i],
                originalRotations[i]
            ));
        }

        cleared = true;
    }

    IEnumerator MoveFurniture(Transform furniture, Vector3 targetPos, Quaternion targetRot)
    {
        Vector3 startPos = furniture.position;
        Quaternion startRot = furniture.rotation;
        float elapsed = 0f;
        float duration = 1f / moveSpeed;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            furniture.position = Vector3.Lerp(startPos, targetPos, t);
            furniture.rotation = Quaternion.Slerp(startRot, targetRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        furniture.position = targetPos;
        furniture.rotation = targetRot;
    }
}