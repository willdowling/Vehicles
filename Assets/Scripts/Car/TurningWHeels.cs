using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningWHeels : MonoBehaviour
{
    [Header("Wheels")]
    public GameObject wheel;
    private Rigidbody rb;
    private bool fwrd;

    // Start is called before the first frame update
    void Start()
    {

        rb = transform.root.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.magnitude > 0f && Input.GetAxis("Vertical") > 0)
        {
            fwrd = true;
            wheel.transform.Rotate(-rb.velocity.magnitude / 2, 0.0f, 0.0f);
        }
        else if (rb.velocity.magnitude > 0f && Input.GetAxis("Vertical") < 0)
        {
            fwrd = false;
            wheel.transform.Rotate(rb.velocity.magnitude / 2, 0.0f, 0.0f);
        }
        else if (fwrd)
        {

            wheel.transform.Rotate(-rb.velocity.magnitude / 2, 0.0f, 0.0f);
        }
        else
        {
            wheel.transform.Rotate(rb.velocity.magnitude / 2, 0.0f, 0.0f);
        }

        
    }
}
