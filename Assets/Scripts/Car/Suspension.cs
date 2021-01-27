using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public bool wheelFrontLeft, wheelFrontRight, wheelRearLeft, wheelRearRight;

    private Rigidbody rb;

    [Header("Handbrake")]
    public float maxHandbrakeForce;
    public float handbrakeForce;

    private bool handbrake;
    private Vector3 velocity;


    [Header("Suspension")]
    public float restLength;
    public float springTravrel;
    public float springStiffness;
    public float damperStiffness;

    private float maxLength;
    private float minLength;
    private float lastLength;
    public float springLength;
    private float springForce;
    private float springVelocity;
    private float damperForce;


    [Header("Wheel")]
    public GameObject wheel;
    public float steerAngle;
    public float steerTime;
    public float wheelRadius;


    private float wheelAngle;
    private Vector3 wheelVelocityLS; // Local space
    private Vector3 suspensionForce;
    private float Fx;
    private float Fy;

    private float heelDist;
    private float suspension;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        
        minLength = restLength - springTravrel;
        maxLength = restLength + springTravrel;
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.green);

        if (Input.GetKey("space")) 
        {
            handbrake = true;
        }
        if(Input.GetKeyUp("space"))
        {
            handbrake = false;
        }
        suspension = wheelRadius - springLength;
        suspension = Mathf.Clamp(suspension, -0.15f, 0.4f);
        if (wheelFrontLeft || wheelFrontRight)
        {
            wheel.transform.localPosition = new Vector3(0.0f, suspension, 0.0f);
        }
        else
        {
            wheel.transform.localPosition = new Vector3(0.0f, suspension + 0.03f, 0.0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;


            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springForce = springStiffness * (restLength - springLength);
            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;
            
           
            damperForce = damperStiffness * springVelocity;
            suspensionForce = (springForce + damperForce) * new Vector3(0.0f,1.0f,0.0f);

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));



            Fx = Input.GetAxis("Vertical") * springForce;
            Fy = wheelVelocityLS.x * springForce;

            velocity = (rb.velocity * handbrakeForce);
            if (handbrake)
            {
                velocity.y = 0.0f;
                rb.AddForceAtPosition(suspensionForce + -velocity + (Fy * -transform.right), hit.point);
                if (handbrakeForce < maxHandbrakeForce)
                {
                    handbrakeForce += 3f;
                }
            }
            else
            {
                rb.AddForceAtPosition((suspensionForce + ((Fx * transform.forward) + (Fy * -transform.right)) - rb.velocity*100), hit.point);
                handbrakeForce = 1;
            }
        }
    }
}
