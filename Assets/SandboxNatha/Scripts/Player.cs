using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{

    public float health;
    public float maxHealth = 10f;

    public float invulnerabilityTime = 1f;
    private float lastDamageTime = 0;

    public HealthBar healthBar;

    public string[] dialogue;
    public string[] npcName;
    public bool showControls = false;

    private PlayerController playerController;

    private MeshRenderer[] renderers;
    private SkinnedMeshRenderer[] skinnedRenderers;

    private PlayerShittyFriendsManager shittyFriendsManager;

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();

        if (showControls)
        {
            DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
        }
        healthBar = GameObject.Find("UI/PlayerHealth").GetComponent<HealthBar>();
        health = maxHealth;

        renderers = GetComponentsInChildren<MeshRenderer>();
        skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        shittyFriendsManager = gameObject.GetComponent<PlayerShittyFriendsManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            shittyFriendsManager.UseShittyFriend();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            shittyFriendsManager.SwitchShittyFriends();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && Time.time - lastDamageTime >invulnerabilityTime)
        {
            float damage = collision.gameObject.GetComponent<Enemy>().contactDamage;
            Damage(damage);
            lastDamageTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastDamageTime > invulnerabilityTime && !playerController.isDashing)
        {
            if (other.CompareTag("EnemyProjectile"))
            {
                float damage = other.gameObject.GetComponent<ProjectileProperties>().damage;
                Damage(damage);
                lastDamageTime = Time.time;
            }
            else if (other.CompareTag("EnemyWeapon"))
            {
                float damage = other.gameObject.GetComponent<IWeapon>().damage;
                Damage(damage);
                lastDamageTime = Time.time;
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar();
        StartCoroutine(nameof(Blink));
    }

    public void Heal(float heal)
    {
        health += heal;
        healthBar.UpdateHealthBar();
    }

    public IEnumerator Blink()
    {
        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = !rend.enabled;
        }
        foreach (SkinnedMeshRenderer rend in skinnedRenderers)
        {
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.1f);

        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = !rend.enabled;
        }
        foreach (SkinnedMeshRenderer rend in skinnedRenderers)
        {
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.1f);
    }
}
