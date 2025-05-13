using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickerLight;
    public float minTime = 0.05f;
    public float maxTime = 0.2f;

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            flickerLight.enabled = !flickerLight.enabled;
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

