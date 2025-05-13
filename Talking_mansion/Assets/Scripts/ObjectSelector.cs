using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public float interactDistance = 5f;
    public LayerMask interactLayer;
    public CrosshairBehavior crosshair; // Reference the CrosshairBehavior script

    void Update()
    {
        if (PauseMenu.IsPaused) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        bool isOverInteractable = false;

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            isOverInteractable = interactable != null;

            if (Input.GetMouseButtonDown(0) && isOverInteractable)
            {
                interactable.Interact();
            }
        }

        crosshair.SetInteractable(isOverInteractable);
    }
}

