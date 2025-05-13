using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Inventory
{
    private static Dictionary<string, InventoryItem> items = new Dictionary<string, InventoryItem>();

    public static void AddItem(InventoryItem item)
    {
        if (!items.ContainsKey(item.id))
        {
            items.Add(item.id, item);
        }
    }

    public static bool HasItem(string id)
    {
        return items.ContainsKey(id);
    }

    public static IEnumerable<InventoryItem> GetItems()
    {
        return items.Values;
    }

    public static InventoryItem GetItem(string id)
    {
        return items.ContainsKey(id) ? items[id] : null;
    }
    public static void RemoveItem(string id)
    {
        if (items.ContainsKey(id))
        {
            items.Remove(id);
        }
    }

}


