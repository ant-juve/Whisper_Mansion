using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehavior : MonoBehaviour
{
    public Image crosshairImage;
    public Color defaultColor = Color.white;
    public Color interactColor = Color.green;

    public void SetInteractable(bool isOverInteractable)
    {
        crosshairImage.color = isOverInteractable ? interactColor : defaultColor;
    }
}

