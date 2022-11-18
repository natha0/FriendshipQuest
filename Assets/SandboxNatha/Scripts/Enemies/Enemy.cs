using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IDamageable
{
    public float health;
    public float maxHealth = 10f;

    public float contactDamage = 1f;

    public float invulnerabilityTime = 0.5f;
    private float lastDamageTime = 0;

    public GameObject healthBar;
    public Slider slider;

    public int number;
    public delegate void SpawnerCallback(int num);
    SpawnerCallback spawnerCallback;
    public delegate bool IsInRoom(Vector3 position);
    public IsInRoom isInRoom;
    public bool addedToList=false;

    private Vector3 initialPosition;

    public virtual void Start()
    {
        health = maxHealth;
        initialPosition = transform.position;
    }

    public void InitiateProperties(int enemyNumber,SpawnerCallback callback, IsInRoom isInRoomFunction)
    {
        addedToList = true;
        number = enemyNumber;
        spawnerCallback = callback;
        isInRoom = isInRoomFunction;
    }

    public void UpdateHealthBar()
    {
        slider.value = Mathf.Clamp(health / maxHealth, 0, 1f);
        if (health < maxHealth)
        {
            healthBar.SetActive(true);
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
        if((Time.time - lastDamageTime) > invulnerabilityTime)
        {
            if (other.CompareTag("PlayerWeapon"))
            {
                lastDamageTime = Time.time;
                float damage = other.GetComponent<IWeapon>().damage;
                Damage(damage);
            }
            else if (other.CompareTag("AllyProjectile"))
            {
                float damage = other.GetComponent<ProjectileProperties>().damage;
                Damage(damage);
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        UpdateHealthBar();
    }

    public void Heal(float heal)
    {
        health += heal;
        UpdateHealthBar();
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }


}
