using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Suspension[] wheels;

    public Camera camera;
    [Header("Car specs")]
    public float wheelBase; // In meters
    public float rearTrack; // In meters
    public float turnRadius; // In meters

    [Header("Inputs:")]
    public float steerInput;

    public float ackermannAngleLeft;
    public float ackermannAngleRight;

    private float springLength;

    // Update is called once per frame
    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        if (steerInput > 0)
        { // Is turning right
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase/(turnRadius+ (rearTrack/2))) * steerInput;
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0)
        { // Is turning left
            ackermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            ackermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else 
        {
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }
        springLength = 0;
        foreach(Suspension w in wheels)
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
        springLength /= 4;
        camera.transform.localPosition = new Vector3(0.0f, springLength+2.97f, -4.51f);
    }
}
