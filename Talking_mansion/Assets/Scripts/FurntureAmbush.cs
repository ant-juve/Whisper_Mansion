using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FurnitureAmbush : MonoBehaviour
{
    public Transform furniture;              // The furniture to move
    public Transform targetPosition;         // The location to block the door
    public float moveSpeed = 2f;             // How fast the furniture moves

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(MoveFurniture());
        }
    }

    private IEnumerator MoveFurniture()
    {
        while (Vector3.Distance(furniture.position, targetPosition.position) > 0.05f)
        {
            furniture.position = Vector3.MoveTowards(
                furniture.position,
                targetPosition.position,
                moveSpeed * Time.deltaTime
            );
            furniture.rotation = Quaternion.Slerp(
                furniture.rotation,
                targetPosition.rotation,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Snap into exact final position
        furniture.position = targetPosition.position;
        furniture.rotation = targetPosition.rotation;
    }
}

