using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float health, maxHealth;
    public HealthBar healthBar;

    public void TakeDamage(float damage)
    {
        // Use your own damage handling code, or this example one.
        health -= damage;
        healthBar.UpdateHealthBar();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1f);
        }
    }
}
