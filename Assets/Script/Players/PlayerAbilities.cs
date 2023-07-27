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
    float timer;
    [SerializeField] GameObject HealingVfx;
    [Header("Cooldowns")]
    [SerializeField] Image imageHealthCooldown;
    [SerializeField] Image imageBlastCooldown;

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
        timer += Time.deltaTime;
        BlastTimer += Time.deltaTime;
        if (Input.GetButtonDown(ult) && BlastTimer >= TimeBetweenBlast)
        {
            if (healingCast == true)
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
            if (blasting == false)
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
            if (blasting == false)
            {
                HealingAnimation();
                timer = 0f;
                isCooldownH = true;
                healingCast = true;
            }
        }
        HealingCooldown();
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
        if (isCooldownB == true)
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
    }
    void ShootBlast()
    {
        Instantiate(Ultprefab, Ultfirepoint.position, Ultfirepoint.rotation);
    }
    void ShootingAnimation()
    {
        anim.SetTrigger("Shooting");
    }

    void ShootingBlastAnimation()
    {
        anim.SetTrigger("Blast");
    }

    void HealingAnimation()
    {
        Instantiate(HealingVfx, Self.position, Self.rotation);
        anim.SetTrigger("Healing");
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
