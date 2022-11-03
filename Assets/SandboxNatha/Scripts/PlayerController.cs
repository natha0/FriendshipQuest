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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticallInput = Input.GetAxis("Vertical");

        movementDirection = (verticallInput * Vector3.forward + horizontalInput * Vector3.right).normalized;

        if (movementDirection.magnitude>=0.1)
        {

            transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);

            //transform.rotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), Time.deltaTime * rotationSmoothness);


        }

        






    }
}
