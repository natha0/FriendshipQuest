using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour
{
    private bool isGameOver;

    public Vector3 cameraOffset;
    public Vector3 deltas;

    private float xmin, xmax, zmin, zmax;
    private float x, z;
    private Vector3 pos;

    private Transform playerTransform;

    private void Start()
    {
        isGameOver = false;
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerTransform.gameObject.GetComponent<Player>().gameOver+=GameOver;
    }

    private void Update()
    {
        if (!isGameOver)
        {
            pos = playerTransform.position;
            x = Mathf.Clamp(pos.x, xmin, xmax);
            z = Mathf.Clamp(pos.z, zmin, zmax);

            transform.position = new Vector3(x, pos.y, z) + cameraOffset;
        }
    }

    public void SetRoomBorders(Vector3 roomCenter,Vector3 roomMin,Vector3 roomMax)
    {
        xmin = Mathf.Min(roomMin.x + deltas.x, roomCenter.x);
        xmax = Mathf.Max(roomMax.x - deltas.x, roomCenter.x);
        zmin = Mathf.Min(roomMin.z + deltas.z, roomCenter.z);
        zmax = Mathf.Max(roomMax.z - deltas.z, roomCenter.z);
    }

    private void GameOver()
    {
        isGameOver = true;
    }

}
