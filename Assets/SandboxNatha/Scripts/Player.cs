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

    public string[] dialogue;
    public string npcName;
    public bool showControls = false;

    void Start()
    {
        if (showControls)
        {
            DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
        }
        healthBar = GameObject.Find("UI/PlayerHealth").GetComponent<HealthBar>();
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
        if (Time.time - lastDamageTime > invulnerabilityTime)
        {
            if (other.CompareTag("EnemyProjectile"))
            {
                float damage = other.gameObject.GetComponent<ProjectileProperties>().damage;
                TakeDamage(damage);
                lastDamageTime = Time.time;
            }
            else if (other.CompareTag("EnemyWeapon"))
            {
                float damage = other.gameObject.GetComponent<IWeapon>().damage;
                TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
        
    }

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
            shittyFriendsList[0].GetComponent<ShittyFriendProperties>().UsePower();
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
