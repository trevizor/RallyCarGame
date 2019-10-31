using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeYRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        float newRotation = Random.Range(0f, 360f);
        gameObject.transform.Rotate(0f, newRotation, 0f);
    }

    void OnValidate()
    {
        float newRotation = Random.Range(0f, 360f);
        gameObject.transform.Rotate(0f, newRotation, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
