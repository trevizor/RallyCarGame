using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class Car : MonoBehaviour {
    private Player player;

	public Transform centerOfMass;
	public WheelCollider FRWheel;
	public WheelCollider FLWheel;
	public WheelCollider RRWheel;
	public WheelCollider RLWheel;

	private Rigidbody rBody;
	private float accInput;
    private float brakeInput;
    private float wheelDirection;

    public bool FWD;
    public bool RWD;
    public bool AWD;

    public float engineTorque = 500.0f;
	public float brakeTorque = 200.0f;
	public float maxSteerAngle = 20.0f;

	public float targetAlignment = 0;

	public Vector3 targetDirection;

	private bool isReversing = false;

	void Start () {
		rBody = GetComponent<Rigidbody> ();
        player = ReInput.players.GetPlayer(0);
        Debug.Log(player.descriptiveName);
        rBody.centerOfMass = centerOfMass.localPosition;
	}

    private void Update()
    {
        wheelDirection = player.GetAxis("Wheel");
        accInput = player.GetAxis("Accelerator");
        brakeInput = player.GetAxis("Brake");

    }

    void FixedUpdate () {
        Debug.Log(wheelDirection + " / " + accInput + "/ " + brakeInput);
        Debug.Log(player.GetAxis("Wheel") + " / " + player.GetAxis("Accelerator") + "/ " + player.GetAxis("Brake"));
        if (AWD)
            RWD = FWD = true;

        if (RWD)
        {
            RRWheel.motorTorque = accInput * engineTorque;
            RLWheel.motorTorque = accInput * engineTorque;
        }
        if (FWD) {
            FRWheel.motorTorque = accInput * engineTorque;
            FLWheel.motorTorque = accInput * engineTorque;
        }
        

		if (rBody.velocity.magnitude < 2 && brakeInput > 0) {
			isReversing = true;
		}

		if (accInput != 0 || brakeInput == 0) {
			isReversing = false;
		}

		if (isReversing) {
            if (RWD)
            {
                RRWheel.motorTorque = -brakeInput * engineTorque;
                RLWheel.motorTorque = -brakeInput * engineTorque;
            }
            if (FWD)
            {
                FRWheel.motorTorque = -brakeInput * engineTorque;
                FLWheel.motorTorque = -brakeInput * engineTorque;
            }
		} else {
			FRWheel.brakeTorque = brakeInput * brakeTorque;
			FLWheel.brakeTorque = brakeInput * brakeTorque;
			RRWheel.brakeTorque = brakeInput * brakeTorque;
			RLWheel.brakeTorque = brakeInput * brakeTorque;
		}
        
        FRWheel.steerAngle = wheelDirection * maxSteerAngle;
        FLWheel.steerAngle = wheelDirection * maxSteerAngle;

	}
}
