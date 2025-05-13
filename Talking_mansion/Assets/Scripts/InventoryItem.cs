using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string id;
    public string name;
    public string description;
    public Sprite icon;

    public InventoryItem(string id, string name, string description, Sprite icon = null)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.icon = icon;
    }
}


