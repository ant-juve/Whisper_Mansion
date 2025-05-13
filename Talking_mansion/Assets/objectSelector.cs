using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSelector : MonoBehaviour
{
    public float interactDistance = 5f;
    public LayerMask interactLayer;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            if (Input.GetMouseButtonDown(0)) // Left-click
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                interactable?.Interact();
            }
        }
    }
}
