using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private Player player;

    private Quaternion defaultRotation;
    private Vector3 lastRotation;
    private Vector3 currentRotationSpeed;
    private Vector3 targetRotation;
    public float wheelDirection;
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

        if (lookVec.x != 0 && lookVec.y != 0) //player is interacting
        {
            lastRotation = transform.rotation.eulerAngles;
            targetRotation = Quaternion.LookRotation(lookVec, Vector3.back).eulerAngles;
            if (targetRotation.z >= 0f && targetRotation.z <= 150f ||  //rotated left
                targetRotation.z <= 360 && targetRotation.z >= 210f) //rotating right
            {
                transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);
            }
            
            //currentRotationSpeed = lastRotation - transform.rotation.eulerAngles;
        } else
        { //player is not interacting
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, 0.05f);
        }


        float zRotation = transform.rotation.eulerAngles.z;
        wheelDirection = 0f;
        if (zRotation < 150f) //left rotation
        {
            wheelDirection = zRotation / 150 * -1;
        }
        else
        {
            if (zRotation > 210f)
            {
                wheelDirection = 1 - ((zRotation - 210) / 150);
            }
        }        
    }
}
