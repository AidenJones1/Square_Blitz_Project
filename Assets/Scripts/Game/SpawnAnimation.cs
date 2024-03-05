using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimation : MonoBehaviour
{
    Vector3 captureObjectScale;
    Vector3 currentObjectScale;

    private void Awake()
    {
        captureObjectScale = gameObject.transform.localScale;        
    }

    private void Update()
    {
        var growthSpeed = 1.7f;

        if (currentObjectScale.x < captureObjectScale.x)
        {
            currentObjectScale.x += growthSpeed * Time.deltaTime;
            currentObjectScale.y += growthSpeed * Time.deltaTime;

            gameObject.transform.localScale = new Vector3(currentObjectScale.x, currentObjectScale.y, currentObjectScale.z);

            if (currentObjectScale.x >= captureObjectScale.x)
            {
                gameObject.transform.localScale = new Vector3(captureObjectScale.x, captureObjectScale.y, captureObjectScale.z);
            }
        }
    }
}
