using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public bool isGameOver;
    private GameObject gameOverPanel;

    public float health;
    public float maxHealth = 10f;
    public float invulnerabilityTime = 1f;
    private bool damageable = true;

    public HealthBar healthBar;

    public DialogueLine[] dialogue;
    public bool showControls = false;

    private PlayerController playerController;

    private MeshRenderer[] renderers;
    private SkinnedMeshRenderer[] skinnedRenderers;

    private PlayerShittyFriendsManager shittyFriendsManager;

    public AudioManager audioManager;

    [HideInInspector] public GameObject BillyProtector=null;

    private ParticleSystem system;

    public delegate void Teleport(Vector3 previousPosition,Vector3 newPosition);
    public Teleport teleport;

    public delegate void doGameOver();
    public doGameOver gameOver;
    private bool DeactivateGameOver => GodModeManager.Instance.DeactivateGameOver;

    void Start()
    {
        gameOverPanel = GameObject.Find("UI/GameOver");
        gameOverPanel.SetActive(false);
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        playerController = gameObject.GetComponent<PlayerController>();

        if (showControls)
        {
            DialogueSystem.Instance.AddNewDialogue(dialogue);
        }
        healthBar = GameObject.Find("UI/PlayerHealth").GetComponent<HealthBar>();
        health = maxHealth;
        damageable = true;

        renderers = GetComponentsInChildren<MeshRenderer>();
        skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        shittyFriendsManager = gameObject.GetComponent<PlayerShittyFriendsManager>();

        system = GetComponent<ParticleSystem>();
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
        if (BillyProtector == null)
        {
            audioManager.Play("Player Hit");
            health -= damage;
            healthBar.UpdateHealthBar();
            if (health <= 0 && !DeactivateGameOver)
            {
                health = 0;
                GameOver();
            }
        }
        else
        {
            Destroy(BillyProtector);
            BillyProtector = null;
        }

        damageable = false;
        Invoke(nameof(ResetDamageable), invulnerabilityTime);
        StartCoroutine(nameof(Blink));
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
        if (system != null)
        {
            system.Play();
        }

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

    public void AddProtector(string type,GameObject protector=null)
    {
        switch (type)
        {
            case "Billy":
                BillyProtector = protector;
                break;
        }
    }

    public void TeleportPlayer(Vector3 position)
    {
        Vector3 previousPos = transform.position;
        if (playerController.isDashing)
        {
            playerController.tr.emitting = false;
        }
        transform.position = position;
        teleport?.Invoke(previousPosition:previousPos ,newPosition:position);
    }

    void GameOver()
    {
        isGameOver = true;
        Destroy(gameObject);
        gameOverPanel.SetActive(true);
        gameOver?.Invoke();
    }
}
