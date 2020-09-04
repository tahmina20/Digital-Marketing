using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerCar : MonoBehaviour {

    // Control movement
    private bool canMove = true;

    // Physics
    private BoxCollider playerBoxCollider;
    private Rigidbody rigidBody;

    //Sounds
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip motorLoop, motorOff, collisionSound; 

    private void Awake()
    {
        playerBoxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = motorLoop;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }

    private void OnEnabled()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
 
    }
        
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    // Control wheel transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform wheel_T)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheel_T.position = position;
        wheel_T.rotation = rotation;
    }

    public void FixedUpdate()
    {
        if (!canMove)
            return;

        if (CrossPlatformInputManager.GetButton("Accelerate"))
        {
            audioSource.pitch = 1f;
        }
        else if(CrossPlatformInputManager.GetButton("Brake"))
        {
            audioSource.pitch = 0.95f;
        }
        else
        {
            audioSource.pitch = 0.9f;
        }

        // Control wheel collider
        float motor = CrossPlatformInputManager.GetButton("Accelerate") ? maxMotorTorque : 
            (CrossPlatformInputManager.GetButton("Brake") ? -maxMotorTorque : 0);
        float steering = maxSteeringAngle * CrossPlatformInputManager.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.leftWheel_T);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.rightWheel_T);
        }
    }

    // Manage collision
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CollisionFail")
        {
            canMove = false;

            audioSource.pitch = 0.9f;
            audioSource.Stop();
            audioSource.PlayOneShot(collisionSound);
            // Stop the car
            rigidBody.isKinematic = true;
            GameObject.Find("GameMgr").GetComponent<GameMgr>().Lose();

            
        }   
    }

    // Check win condition
    public void OnTriggerStay(Collider collider)
    {
        if(playerBoxCollider.bounds.min.x > collider.bounds.min.x &&
           playerBoxCollider.bounds.max.x < collider.bounds.max.x &&

           playerBoxCollider.bounds.min.y > collider.bounds.min.y &&
           playerBoxCollider.bounds.max.y < collider.bounds.max.y &&

           playerBoxCollider.bounds.min.z > collider.bounds.min.z &&
           playerBoxCollider.bounds.max.z < collider.bounds.max.z &&
           
           canMove)
        {
            canMove = false;
            // Stop the car
            rigidBody.isKinematic = true;
            audioSource.pitch = 0.9f;
            audioSource.Stop();
            audioSource.PlayOneShot(motorOff);
            
            GameObject.Find("GameMgr").GetComponent<GameMgr>().Win();
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public Transform leftWheel_T;
    public Transform rightWheel_T;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}

