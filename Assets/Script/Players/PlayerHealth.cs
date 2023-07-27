using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int startingHealth = 200;
    public int currentHealth;
    private int LimitHealth = 300;
    [SerializeField] Slider HealthSlider;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip NewSong;

    [SerializeField] int HealthAmountPickup;
    [SerializeField] int HealthAmountSpell;

    float speedtimer = 0f;
    float oldvelocity; //velocidad guardada
    float newvelocity; //velocidad guardad y duplicarla.
    int counting = 0;

    Animator anim;
    PlayerMovement playerMovement;
    PlayerAbilities PlayerAbilities;
    bool isDead;
    //bool damaged; //booleano para avisar que he sido lastimado.  

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        PlayerAbilities = GetComponent<PlayerAbilities>();
        currentHealth = startingHealth;
        audio = GetComponentInParent<AudioSource>();
        audio.Play();
        oldvelocity = playerMovement.velocity; //obtener velocidad
        newvelocity = oldvelocity * 2; //crear el nuevo valor de velocidad.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            currentHealth += HealthAmountPickup;
            HealthSlider.value = currentHealth;
        }
    }

    private void Update()
    {
        if (currentHealth > LimitHealth)
        {
            currentHealth = LimitHealth;
        }
        Timedragged();
    }

    public void TakeDamage(int amount)
    {

        if (PlayerAbilities.blasting == true) // invulnerabilidad
        {
            audio.clip = NewSong;
            audio.Play();
            return;
        }
        else //recibir daño
        {
            counting += 1;
            currentHealth -= amount;
            HealthSlider.value = currentHealth;
            if (counting == 3)
            {
                Speedingup();
            }
        }


        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerMovement.enabled = false;
        FindObjectOfType<MenusinGame>().EndGame();
    }
    public void Healing()
    {
        if (currentHealth > 0)
        {
            currentHealth += HealthAmountSpell;
            HealthSlider.value = currentHealth;
        }
    }
    void Speedingup() //incrementar velocidad
    {
        speedtimer = 0f;
        counting = 0;
        playerMovement.velocity = newvelocity;    
    }
    void Timedragged() //
    {
        speedtimer += Time.deltaTime;
        if (speedtimer >= 2f && newvelocity == playerMovement.velocity) 
        {
            playerMovement.velocity = oldvelocity;
            return;
        }

    }


    //invulnerabilidad == invul health = current health
    //current health = invul. 
}
