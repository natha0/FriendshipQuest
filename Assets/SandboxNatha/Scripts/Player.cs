using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamageable
{

    public float health;
    public float maxHealth = 10f;
    public float invulnerabilityTime = 1f;
    private bool damageable = true;

    public HealthBar healthBar;

    public string[] dialogue;
    public string[] npcName;
    public bool showControls = false;

    private PlayerController playerController;

    private MeshRenderer[] renderers;
    private SkinnedMeshRenderer[] skinnedRenderers;

    private PlayerShittyFriendsManager shittyFriendsManager;

    private bool deactivateGameOver => GodModeManager.Instance.deactivateGameOver;

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();

        if (showControls)
        {
            DialogueSystem.Instance.AddNewDialogue(dialogue, npcName);
        }
        healthBar = GameObject.Find("UI/PlayerHealth").GetComponent<HealthBar>();
        health = maxHealth;
        damageable = true;

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
        if(collision.gameObject.CompareTag("Enemy") && damageable)
        {
            float damage = collision.gameObject.GetComponent<Enemy>().contactDamage;
            Damage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damageable && !playerController.isDashing)
        {
            if (other.CompareTag("EnemyProjectile"))
            {
                float damage = other.gameObject.GetComponent<ProjectileProperties>().damage;
                Damage(damage);
            }
            else if (other.CompareTag("EnemyWeapon"))
            {
                float damage = other.gameObject.GetComponent<IWeapon>().damage;
                Damage(damage);
            }
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar();
        damageable = false;
        Invoke(nameof(ResetDamageable), invulnerabilityTime);
        StartCoroutine(nameof(Blink));
        if (health <= 0 && !deactivateGameOver)
        {
            GameOver();
        }
    }

    void ResetDamageable()
    {
        damageable = true;
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.UpdateHealthBar();
    }

    public IEnumerator Blink()
    {
        while (!damageable)
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
        }

        foreach (MeshRenderer rend in renderers)
        {
            rend.enabled = true;
        }
        foreach (SkinnedMeshRenderer rend in skinnedRenderers)
        {
            rend.enabled = true;

        }
    }

    void GameOver()
    {
        print("gameOver");
    }
}
