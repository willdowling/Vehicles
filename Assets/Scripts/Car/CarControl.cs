using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Camera camera;
    public GameObject steeringWheel;

    [Header("Car specs")]
    public float wheelBase; // In meters
    public float rearTrack; // In meters
    public float turnRadius; // In meters

    [Header("Tyer Values")]
    public Suspension[] wheels;

    public float ackermannAngleLeft;
    public float ackermannAngleRight;

    public float springLength;

    [Header("Inputs:")]
    public float steerInput;

    // Update is called once per frame
    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        steeringWheel.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,1f) * (Mathf.Asin(-steerInput)*(180f / Mathf.PI)));

        (ackermannAngleLeft, ackermannAngleRight) = GetAckermannAngle(wheelBase, turnRadius, rearTrack, steerInput);

        ApplySuspension(ackermannAngleLeft, ackermannAngleRight);
    }

    private void ApplySuspension(float ackermannAngleLeft, float ackermannAngleRight)
    {
        springLength = 0;
        foreach (Suspension w in wheels)
        {

            springLength += w.springLength;
            if (w.wheelFrontLeft)
            {
                springLength += w.springLength;
                w.steerAngle = ackermannAngleLeft;
            }
            if (w.wheelFrontRight)
            {
                w.steerAngle = ackermannAngleRight;
            }
        }
    }

    private (float, float) GetAckermannAngle(float wheelBase, float turnRadius, float rearTrack, float steerInput)
    {
        ackermannAngleLeft = 0;
        ackermannAngleRight = 0;
        if (steerInput > 0)
        { // Is turning right
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0)
        { // Is turning left
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        return (ackermannAngleLeft, ackermannAngleRight);
    }
}
