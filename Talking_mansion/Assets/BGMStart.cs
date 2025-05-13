using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMStart : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Playing BGM...");
        GetComponent<AudioSource>().Play();
    }
}

