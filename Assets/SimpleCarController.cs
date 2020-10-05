
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
     
public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;
     
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
     
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        //testing tank driving by getting rid of steering and put motors on different inputs
        foreach (AxleInfo axleInfo in axleInfos) {
            /*if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }*/

            if (axleInfo.motor) {
                if (Input.GetKey("w") || Input.GetKey("s"))
                {
                    axleInfo.leftWheel.motorTorque = motor;
                }
                else if (Input.GetKey("i") || Input.GetKey("k"))
                {
                    axleInfo.rightWheel.motorTorque = motor;
                }
                //axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}

/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Wheel Collider Reference: https://docs.unity3d.com/Manual/class-WheelCollider.html
//WHeel Collider Tutorial Reference: https://docs.unity3d.com/Manual/WheelColliderTutorial.html

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
     
public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
    public float maxMotorTorque;
    public float maxSteeringAngle;
     
    //Finds each wheel collider's attached Visual Wheel Gameobject and updates it's rotation/visuals
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
     
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Horizontal");
        float brake = maxSteeringAngle * Input.GetAxis("Horizontal");
        float steering = 0;

        Debug.Log("Motor: " + motor);
     
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                if (motor > 0) {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = 0;
                } else if (motor < 0) {
                    axleInfo.rightWheel.motorTorque = Mathf.Abs(motor);
                    axleInfo.leftWheel.motorTorque = 0;
                } else if (motor == 0) {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                } else if (brake < 0) {
                    axleInfo.leftWheel.motorTorque = brake;
                    axleInfo.rightWheel.motorTorque = brake;
                }
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}*/
