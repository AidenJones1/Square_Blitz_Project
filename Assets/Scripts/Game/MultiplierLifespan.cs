using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierLifespan : MonoBehaviour
{
    [SerializeField] private float _lifespanDuration = 2f;

    private void Start()
    {
        StartCoroutine(LifespanSequence());
    }

    private IEnumerator LifespanSequence()
    {
        yield return new WaitForSecondsRealtime(_lifespanDuration);
        Destroy(gameObject);
    }
}
