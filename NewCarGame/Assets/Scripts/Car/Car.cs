using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine;

public class Car : MonoBehaviour {
    private Player player;

	public Transform centerOfMass;
	public WheelCollider FRWheel;
	public WheelCollider FLWheel;
	public WheelCollider RRWheel;
	public WheelCollider RLWheel;

    public GameObject WheelSprite;

	private Rigidbody rBody;
	private float accInput;
    private float brakeInput;
    private float wheelDirection;

    public bool FWD;
    public bool RWD;
    public bool AWD;

    //GEARS
    private static float[] GearRatio =
           new float[]{
               3.708f,
               2.410f,
               1.722f,
               1.294f,
               1.023f,
               0.849f
           };
    private float ReverseGearRatio = 3.708f;
    public float MaxEngineRPM = 3000.0f;
    public float MinEngineRPM  = 1000.0f;
    private float EngineRPM = 0.0f;
    [HideInInspector]
    public int CurrentGear = 0;

    //make flips better
    private Vector3 defaultCenterOfMass;
    private bool fourWheelsOnGround = false;

    public float engineTorque = 500.0f;
	public float brakeTorque = 200.0f;
	public float maxSteerAngle = 20.0f;

	public float targetAlignment = 0;

	public Vector3 targetDirection;

	private bool isReversing = false;
    private WheelController wheelController;

	void Start () {
		rBody = GetComponent<Rigidbody> ();
        player = ReInput.players.GetPlayer(0);
        defaultCenterOfMass = rBody.centerOfMass;
        rBody.centerOfMass = centerOfMass.localPosition;
        wheelController = WheelSprite.GetComponent<WheelController>();
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        wheelDirection = GameObject.FindObjectOfType<WheelController>().wheelDirection; //maneiro controls
        accInput = player.GetAxis("Accelerator");
        brakeInput = player.GetAxis("Brake");
    }

    void CalculateCenterOfMassPosition ()
    {
        if (FRWheel.isGrounded && FLWheel.isGrounded && RRWheel.isGrounded && RLWheel.isGrounded) //changes the center of mass to create cool flips and crashes when not touching the ground
        {
            fourWheelsOnGround = true;
            rBody.centerOfMass = (centerOfMass.localPosition + rBody.centerOfMass * 3) / 4;  //it takes 4 physics frames to fully change the center of mass location, to prevent crazy motions
        }
        else
        {
            fourWheelsOnGround = false;
            rBody.centerOfMass = (defaultCenterOfMass + rBody.centerOfMass * 3) / 4;
        }
    }

    void FixedUpdate () {

        CalculateCenterOfMassPosition();

        float engineTorqueOnCurrentGear = engineTorque * GearRatio[CurrentGear];

        if (AWD)
            RWD = FWD = true;
        
        if (RWD)
        {
            RRWheel.motorTorque = accInput * engineTorqueOnCurrentGear;
            RLWheel.motorTorque = accInput * engineTorqueOnCurrentGear;
            EngineRPM = (RRWheel.rpm + RLWheel.rpm) / 2 * GearRatio[CurrentGear];
        }
        if (FWD) {
            FRWheel.motorTorque = accInput * engineTorqueOnCurrentGear;
            FLWheel.motorTorque = accInput * engineTorqueOnCurrentGear;
            EngineRPM = (FLWheel.rpm + FRWheel.rpm) / 2 * GearRatio[CurrentGear];
        }
        if (AWD)
            EngineRPM = (FLWheel.rpm + FRWheel.rpm + RRWheel.rpm + RLWheel.rpm) / 4 * GearRatio[CurrentGear]; //


        Debug.Log(EngineRPM + " RPM // gear: " + CurrentGear);
        ShiftGears();

        //TODO: create reverse button on interface
        if (rBody.velocity.magnitude < 2 && brakeInput > 0) {
			isReversing = true;
		}

		if (accInput != 0 || brakeInput == 0) {
			isReversing = false;
		}

        if (isReversing) {
            if (RWD)
            {
                RRWheel.motorTorque = -brakeInput * engineTorque * ReverseGearRatio;
                RLWheel.motorTorque = -brakeInput * engineTorque * ReverseGearRatio;
            }
            if (FWD)
            {
                FRWheel.motorTorque = -brakeInput * engineTorque * ReverseGearRatio;
                FLWheel.motorTorque = -brakeInput * engineTorque * ReverseGearRatio;
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

    void ShiftGears()
    {
        // this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
        // the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
        int AppropriateGear = CurrentGear;
        WheelCollider targetWheel;
        if (FWD)
        {
            targetWheel = FLWheel;
        } else
        {
            targetWheel = RLWheel;
        }
        
        if (EngineRPM >= MaxEngineRPM)
        {
            

            for (var i = 0; i < GearRatio.Length; i++)
            {
                if (targetWheel.rpm * GearRatio[i] < MaxEngineRPM)
                {
                    AppropriateGear = i;
                    break;
                }
            }

            CurrentGear = AppropriateGear;
        }

        if (EngineRPM <= MinEngineRPM)
        {
            AppropriateGear = CurrentGear;

            for (var j = GearRatio.Length - 1; j >= 0; j--)
            {
                if (targetWheel.rpm * GearRatio[j] > MinEngineRPM)
                {
                    AppropriateGear = j;
                    break;
                }
            }

            CurrentGear = AppropriateGear;
        }
    }
}
