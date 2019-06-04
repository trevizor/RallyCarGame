using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private float lastRotation;
    private bool isDirectionRight = false;
    private int turnCountRight = 0;
    private int turnCountLeft = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.gameObject.transform.rotation.eulerAngles);
    }
}
