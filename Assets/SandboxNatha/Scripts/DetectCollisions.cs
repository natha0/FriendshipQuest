using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{

    public Image healthBarImage;
    public Player player;
    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = Mathf.Clamp(player.health / player.maxHealth, 0, 1f);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
