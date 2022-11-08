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

    public int number;
    public delegate void SpawnerCallback(int num);
    SpawnerCallback spawnerCallback;
    public bool addedToList=false;

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

    public void InitiateProperties(int enemyNumber,SpawnerCallback callback)
    {
        addedToList = true;
        number = enemyNumber;
        spawnerCallback = callback;
    }

    private void updateHealth(float damage)
    {
        health -= damage;
        slider.value = Mathf.Clamp(health / maxHealth, 0, 1f);
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            spawnerCallback(number);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && Time.time - lastDamageTime > invulnerabilityTime)
        {
            lastDamageTime = Time.time;
            int damage = other.GetComponent<IWeapon>().GetWeaponDamage();
            updateHealth(damage);
        }
        else if (other.CompareTag("AllyProjectile"))
        {
            float damage = other.GetComponent<ProjectileProperties>().damage;
            updateHealth(damage);
        }
    }
}
