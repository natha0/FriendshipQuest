using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billy : ShittyFriend,IShittyFriends
{
    Player player;
    Transform playerTransform;

    [HideInInspector] public bool protectsPlayer;
    public float floatingRange=1;
    public float floatingFrequency = 2;

    private void Start()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        playerTransform = playerObject.GetComponent<Transform>();

    }

    public bool UsePower()
    {
        bool PowerUsed = false;
        if (player.BillyProtector == null)
        {
            GameObject BillyProtector = Instantiate(gameObject, transform.position, Quaternion.identity);
            BillyProtector.GetComponent<Billy>().protectsPlayer = true;
            player.AddProtector(type, BillyProtector);
            PowerUsed = true;
        }
        return PowerUsed;
    }

    public override void Update()
    {
        if (protectsPlayer)
        {
            float phase = 2 * Mathf.PI * Time.time * floatingFrequency;
            Vector3 targetPos = floatingRange* new Vector3(Mathf.Cos(phase),0,Mathf.Sin(phase));

            transform.position = playerTransform.position + targetPos;
        }
        else
        {
            base.Update();
        }
    }


}
