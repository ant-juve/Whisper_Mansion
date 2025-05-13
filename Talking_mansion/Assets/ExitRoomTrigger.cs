using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoomTrigger : MonoBehaviour
{
    public FurnitureEnemy furnitureEnemy; // drag the candle stick enemy here in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            furnitureEnemy.StopChase();
        }
    }
}

