using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProperties : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;

    public float playerDamage = 1f;

    public float invulnerabilityTime = 0.5f;
    private float lastDamageTime = 0;

    public GameObject healthBarUI;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarUI.transform.LookAt(Camera.main.transform);
    }

    private void updateHealth(float damage)
    {
        health -= damage;
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        slider.value = Mathf.Clamp(health / maxHealth, 0, 1f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && Time.time - lastDamageTime > invulnerabilityTime)
        {
            lastDamageTime = Time.time;
            float damage = other.GetComponent<WeaponProperties>().damage;
            updateHealth(damage);
        }
    }
}
