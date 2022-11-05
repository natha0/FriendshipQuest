using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private Vector3 cameraOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraOffset = Camera.main.GetComponent<CameraProperties>().cameraOffset;
            Camera.main.transform.position = transform.position + cameraOffset;
        }
    }

}
