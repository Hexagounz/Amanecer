using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    [Header("F. Attributes")] //firerate,timebetweenblast y timebetween health son publicos para permitir la interaccion con buffs.
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
    float healTimer;
    [SerializeField] GameObject HealingVfx;
    [Header("Cooldowns")]
    [SerializeField] Image imageHealthCooldown;
    [SerializeField] Image imageBlastCooldown;

    [Space] [Header("SFX")] 
    [SerializeField] private AudioSource basicShootSfx;

    [SerializeField] private AudioSource ultimateShootSfx;
    [SerializeField] private AudioSource healSfx;

    [Space] [Header("Power Fx")] 
    [SerializeField] private ParticleSystem ultimateShootParticles;

    public bool canHeal;
    public bool canShoot;
    public bool canBlast;

    bool isCooldownH, isCooldownB;
    public bool healingCast; //para marcar eventos que ocurren o dejan de ocurrir solo cuando la cura esta siendo casteada.
    public bool blasting;

    Animator anim;
    Transform Self;
    PlayerHealth playerHealth;

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
        healTimer += Time.deltaTime;
        BlastTimer += Time.deltaTime;

        BlastCooldown();
        
        HealingCooldown();
    }

    public void DefinitiveShoot()
    {
        if(!canBlast)
            return;
        
        if (BlastTimer >= TimeBetweenBlast)
        {
            if (healingCast)
            {
                return;
            }

            blasting = true;
            ShootingBlastAnimation();
            BlastTimer = 0f;
            isCooldownB = true;

        }
    }

    public void BasicShoot()
    {
        if(!canShoot)
            return;
        
        if (fireCountdown <= 0f)
        {
            if (blasting)
            {
                return;
            }
            ShootingAnimation();
            fireCountdown = 1f / fireRate;
        }
    }

    public void PerformHeal()
    {
        if(!canHeal)
            return;
        
        if (healTimer >= TimeBetweenHealth)
        {
            if (blasting)
            {
                return;
            }
            
            HealingAnimation();
            healTimer = 0f;
            isCooldownH = true;
            healingCast = true;
            
        }
    }
    
    
    void HealingCooldown()
    {
        if (isCooldownH == true)
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
    }
    void ShootBlast()
    {
        Instantiate(Ultprefab, Ultfirepoint.position, Ultfirepoint.rotation);
        ultimateShootSfx.Play();

    }
    void ShootingAnimation()
    {
        anim.SetTrigger("Shooting");
    }

    void ShootingBlastAnimation()
    {
        anim.SetTrigger("Blast");
        ultimateShootParticles.Play();
    }

    void HealingAnimation()
    {
        Instantiate(HealingVfx, Self.position, Self.rotation);
        anim.SetTrigger("Healing");
        healSfx.Play();
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
