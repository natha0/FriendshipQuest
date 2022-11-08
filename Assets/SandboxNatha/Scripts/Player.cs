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

    public List<GameObject> shittyFriendsList = new();
    public List<GameObject> ShittyFriendsType = new();

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar();
    }

    public void HealPlayer(float heal)
    {
        health += heal;
        healthBar.UpdateHealthBar();
    }

    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1) && shittyFriendsList.Count>=1)
        {
            UseShittyFriend();
        }
        if (Input.GetKeyDown(KeyCode.E)  && shittyFriendsList.Count >= 1)
        {
            SwitchShittyFriends();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile") && Time.time - lastDamageTime > invulnerabilityTime)
        {
            float damage = other.gameObject.GetComponent<ProjectileProperties>().damage;
            TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }

    public void AddShittyFriend(GameObject shittyFriend)
    {
        shittyFriendsList.Add(shittyFriend);
    }

    public GameObject GetShittyFriend(int id)
    {
        if (shittyFriendsList.Count>=id)
        {
            return shittyFriendsList[id];
        }
        Debug.LogWarningFormat("The shitty friend with id: {0} doesn't exist!", id);
        return null;
    }

    public void UseShittyFriend()
    {
        if (shittyFriendsList.Count >= 1)
        {
            shittyFriendsList[0].GetComponent<ShittyFriendProperties>().usePower();
            Destroy(shittyFriendsList[0]);
            shittyFriendsList.RemoveAt(0);

            foreach (GameObject shittyFriend in shittyFriendsList)
            {
                shittyFriend.GetComponent<ShittyFriendProperties>().playerNumber--;
            }
        }
    }

    public void SwitchShittyFriends()
    {
        shittyFriendsList.Add(shittyFriendsList[0]);
        shittyFriendsList.RemoveAt(0);

        for (int i=0;i< shittyFriendsList.Count; i++)
        {
            shittyFriendsList[i].GetComponent<ShittyFriendProperties>().playerNumber = i;
        }
    }

}
