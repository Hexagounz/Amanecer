using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerAbilities : MonoBehaviour
{
    [Header("F. Attributes")]
    public float fireRate = 1.5f;
    private float fireCountdown = 0f;
    public float TimeBetweenBlast = 10;
    float BlastTimer = 0f;

    [Header("Shooting Objects")]
    [SerializeField] Transform firepoint;
    [SerializeField] Transform Ultfirepoint;
    [SerializeField] GameObject bulletprefab;
    [SerializeField] GameObject Ultprefab;
    [SerializeField] string fire = "LFire", heal = "LHeal", ult = "LUlti";

    [Header("H. Attributes")]
    public float TimeBetweenHealth = 5;
    float timer;
    [SerializeField] GameObject HealingVfx;

    [Header("Cooldowns")]
    [SerializeField] Image imageHealthCooldown;
    [SerializeField] Image imageBlastCooldown;

    bool isCooldownH, isCooldownB;
    public bool healingCast;
    public bool blasting;

    Animator anim;
    Transform Self;
    PlayerHealth playerHealth;

    [Header("Basic Attack")]
    public float basicAttackRadius = 2f;
    public int basicAttackDamage = 10;
    public float basicAttackDebuffDuration = 2f;
    public float enemyDebuffFactor = 0.5f;

    [Header("Ability 1")]
    public string ability1Button = "Ability1";
    public float ability1Duration = 5f;
    public int ability1HealAmount = 20;

    [Header("Ultimate Ability")]
    public string ultimateButton = "Ultimate";
    public GameObject shieldPrefab;
    public float shieldDuration = 5f;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        Self = transform;
        anim = GetComponent<Animator>();
        isCooldownB = true;
        isCooldownH = true;
    }
    void Update()
    {
        fireCountdown -= Time.deltaTime;
        timer += Time.deltaTime;
        BlastTimer += Time.deltaTime;

        if (Input.GetButtonDown(ult) && BlastTimer >= TimeBetweenBlast)
        {
            if (healingCast)
            {
                return;
            }
            else
            {
                blasting = true;
                ShootingBlastAnimation();
                BlastTimer = 0f;
                isCooldownB = true;
            }
        }

        BlastCooldown();

        if (Input.GetButtonDown(fire) && fireCountdown <= 0f)
        {
            if (!blasting)
            {
                ShootingAnimation();
                fireCountdown = 1f / fireRate;
            }
            else
            {
                return;
            }
        }
        else if (Input.GetButton(heal) && timer >= TimeBetweenHealth)
        {
            if (!blasting)
            {
                HealingAnimation();
                timer = 0f;
                isCooldownH = true;
                healingCast = true;
            }
        }

        HealingCooldown();

        // Habilidad definitiva: Generación de escudo
        if (Input.GetButtonDown(ultimateButton) && !blasting && !healingCast)
        {
            GenerateShield();
        }
    }

    void HealingCooldown()
    {
        if (isCooldownH)
        {
            imageHealthCooldown.fillAmount += 1 / TimeBetweenHealth * Time.deltaTime;
            if (imageHealthCooldown.fillAmount >= 1)
            {
                imageHealthCooldown.fillAmount = 0;
                isCooldownH = false;
            }
        }
    }

    void BlastCooldown()
    {
        if (isCooldownB)
        {
            imageBlastCooldown.fillAmount += 1 / TimeBetweenBlast * Time.deltaTime;
            if (imageBlastCooldown.fillAmount >= 1)
            {
                imageBlastCooldown.fillAmount = 0;
                isCooldownB = false;
            }
        }
    }

    void Shoot() //shoot y shootblast ocurren en medio de la animacion
    {
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        basicShootSfx.Play();
        foreach (Collider2D enemyCollider in enemiesHit)
        {
            EnemyMovement enemyMovement = enemyCollider.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.ApplyDebuff(enemyDebuffFactor, basicAttackDebuffDuration);
            }
        }
    }

    void HealingAnimation()
    {
        Instantiate(HealingVfx, Self.position, Self.rotation);
        anim.SetTrigger("Healing");
        healSfx.Play();
        if (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            playerHealth.Heal(ability1HealAmount);
        }
    }

    void GenerateShield()
    {
        GameObject shield = Instantiate(shieldPrefab, Self.position, Quaternion.identity);
        Destroy(shield, shieldDuration);
    }

    void ShootBlastFinished()  //shootblastfinished y healingfinished ocurren en medio de la animacion.
    {
        blasting = false;
    }

    void HealingFinished()
    {
        healingCast = false;
    }

}
