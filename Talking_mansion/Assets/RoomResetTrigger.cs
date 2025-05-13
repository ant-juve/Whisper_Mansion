using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomResetTrigger : MonoBehaviour
{
    public FurnitureEnemy enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.ResetChase();
        }
    }
}

