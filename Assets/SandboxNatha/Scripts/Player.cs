using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float health;
    public float maxHealth = 10f;

    public float invulnerabilityTime = 1f;
    private float lastDamageTime = 0;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && Time.time - lastDamageTime >invulnerabilityTime)
        {

            float damage = collision.gameObject.GetComponent<EnemyProperties>().playerDamage;
            TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }
}
