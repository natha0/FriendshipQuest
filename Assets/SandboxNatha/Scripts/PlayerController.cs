using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float rotationSmoothness=40f;
    private float horizontalInput;
    private float verticallInput;

    private Vector3 movementDirection;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticallInput = Input.GetAxis("Vertical");

        movementDirection = (verticallInput * Vector3.forward + horizontalInput * Vector3.right).normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = speed * movementDirection;

        if (movementDirection.magnitude>=0.1)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * rotationSmoothness);
        }
        else
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }
}
