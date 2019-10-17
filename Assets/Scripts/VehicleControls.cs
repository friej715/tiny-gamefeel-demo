using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControls : MonoBehaviour
{
    private Rigidbody rb;
    public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        ApplyHorizontalInput();
    }

    private void FixedUpdate()
    {
        ApplyVerticalInput();
    }

    void ApplyHorizontalInput()
    {
        float side = Input.GetAxis("Horizontal");
        Vector3 rotation = new Vector3(0, side, 0);
        transform.Rotate(rotation);
    }

    void ApplyVerticalInput()
    {
        float fwd = Input.GetAxis("Vertical");
        rb.AddForce(transform.forward * fwd);
        currentSpeed = transform.InverseTransformVector(rb.velocity).z;
    }
}
