using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public GameObject car;
    public GameObject plane;
    
    void Start()
    {
        car.SetActive(true);
        plane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            car.SetActive(true);
            plane.SetActive(false);
        }
        if (Input.GetKeyDown("p"))
        {
            car.SetActive(false);
            plane.SetActive(true);
        }
    }
}
