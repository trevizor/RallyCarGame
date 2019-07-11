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
    private float lastTargetRotation;
    private float firstTouchRotation;
    private bool holdingWheel = false;

    private Player player;

    private Quaternion defaultRotation;
    
    private float targetRotation;

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

        if (lookVec.x != 0 || lookVec.y != 0) //player is interacting
        {
            if (!holdingWheel)
            {
                holdingWheel = true;
                firstTouchRotation = Quaternion.LookRotation(lookVec, Vector3.back).eulerAngles.z;
            }
            float currentTargetRotation = Quaternion.LookRotation(lookVec, Vector3.back).eulerAngles.z;
            //targetRotation = currentTargetRotation;
            targetRotation = currentTargetRotation - firstTouchRotation;
            while (targetRotation >= 360f)
            {
                targetRotation -= 360f;
            }
            while (targetRotation < 0)
            {
                targetRotation += 360f;
            }

        } else //player has released the wheel
        {
            holdingWheel = false;
            if (accumulatedRotation != 0)
            {
                targetRotation = accumulatedRotation * 0.94f;
                while (targetRotation > 360f)
                {
                    targetRotation -= 360f;
                }
                while (targetRotation < 0)
                {
                    targetRotation += 360f;
                }
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
        } else
        {
            if (!holdingWheel)
            {
                accumulatedRotation += rotationDiff;
                currentRotation = targetRotation;
                transform.Rotate(new Vector3(0f, 0f, rotationDiff));
            }
        }
        //firstTouchRotation = accumulatedRotation;

        float zRotation = transform.rotation.eulerAngles.z;
        wheelDirection = 0f;
        wheelDirection = (accumulatedRotation / RotationAngle)*-1;
    }

}
