using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyIconHUD : MonoBehaviour
{
    public static KeyIconHUD Instance;

    public GameObject iconPrefab;
    public Transform iconContainer;

    private Dictionary<string, GameObject> activeIcons = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void AddIcon(string keyId, Sprite icon)
    {
        if (!activeIcons.ContainsKey(keyId))
        {
            GameObject newIcon = Instantiate(iconPrefab, iconContainer);
            newIcon.GetComponent<Image>().sprite = icon;
            activeIcons.Add(keyId, newIcon);
        }
    }

    public void RemoveIcon(string keyId)
    {
        if (activeIcons.ContainsKey(keyId))
        {
            Destroy(activeIcons[keyId]);
            activeIcons.Remove(keyId);
        }
    }
}


