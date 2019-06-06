using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public float RotationAngle = 450f;
    [HideInInspector]
    public float wheelDirection;
    private float currentRotation = 0f;
    private float accumulatedRotation = 0f;
    private float lastRotation;
    private float rotationDiff;

    private Player player;

    private Quaternion defaultRotation;
    
    private float targetRotation;
    
    private bool isDirectionRight = false;
    private int turnCountRight = 0;
    private int turnCountLeft = 0;  

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        defaultRotation = transform.rotation;
        wheelDirection = 0;
    }


    public void UpdateRotation ()
    {
        
    }

    void Update()
    {
        Vector3 lookVec = new Vector3(player.GetAxis("WheelRCX"), player.GetAxis("WheelRCY"), 4096);

        lastRotation = currentRotation;

        if (lookVec.x != 0 && lookVec.y != 0) //player is interacting
        {
            targetRotation = Quaternion.LookRotation(lookVec, Vector3.back).eulerAngles.z;
            Debug.Log(targetRotation);
        } else
        {
            if(accumulatedRotation != 0)
            {
                targetRotation = accumulatedRotation * 0.9f;
                while (targetRotation >= 360f)
                {
                    targetRotation -= 360f;
                }
                while (targetRotation < 0)
                {
                    targetRotation += 360f;
                }

                if (targetRotation < 2f || targetRotation > 358f)
                {
                    targetRotation = 0f;
                }
                //Debug.Log(targetRotation);
            }
            
        }


        if (targetRotation > 300f && lastRotation < 60f)
        {
            lastRotation += 360f;
        }
        if (targetRotation < 60f && lastRotation > 300f)
        {
            lastRotation -= 360f;
        }
        rotationDiff = targetRotation - lastRotation;

        if (accumulatedRotation + rotationDiff >= RotationAngle * -1 &&
            accumulatedRotation + rotationDiff <= RotationAngle)
        {
            accumulatedRotation += rotationDiff;
            currentRotation = targetRotation;
            transform.Rotate(new Vector3(0f, 0f, rotationDiff));
        }


        float zRotation = transform.rotation.eulerAngles.z;
        wheelDirection = 0f;
        wheelDirection = (accumulatedRotation / RotationAngle)*-1;
    }
}
