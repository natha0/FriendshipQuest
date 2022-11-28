using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IDamageable
{
    public float health;
    public float maxHealth = 10f;
    public float invulnerabilityTime = 0.5f;
    private bool damageable = true;

    public float contactDamage = 1f;

    public GameObject healthBar;
    public Slider slider;

    [HideInInspector] public int number;
    public delegate void SpawnerCallback(int num);
    SpawnerCallback spawnerCallback;
    public delegate bool IsInRoom(Vector3 position);
    public IsInRoom isInRoom;
    [HideInInspector] public bool addedToList=false;

    private Vector3 initialPosition;

    private MeshRenderer[] renderers;

    private IWeapon weapon;


    public virtual void Start()
    {
        health = maxHealth;
        initialPosition = transform.position;
        renderers = GetComponentsInChildren<MeshRenderer>();
        damageable = true;

        weapon = gameObject.GetComponentInChildren<IWeapon>();
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
        if(damageable)
        {
            if (other.CompareTag("PlayerWeapon"))
            {
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
        damageable = false;
        Invoke(nameof(resetDamageable), invulnerabilityTime);
        StartCoroutine(nameof(Blink));
    }

    void resetDamageable()
    {
        damageable = true;
    }

    public void Heal(float heal)
    {
        health += heal;
        UpdateHealthBar();
    }

    public IEnumerator Blink()
    {
        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.1f);

        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    public virtual void PerformAttack()
    {
        if (weapon != null)
        {
            weapon.PerformAttack();
        }
    }

}
