using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{


    public GameObject player;
    private Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        initialOffset =  transform.position-player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialOffset.magnitude* player.transform.forward ;
    }
}
