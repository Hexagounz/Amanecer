using UnityEngine;
using UnityEngine.UI;

public class LevanaAbilities : MonoBehaviour
{
    [Header("F. Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float TimeBetweenBlast = 10;
    float BlastTimer = 0f;
    [Header("Shooting Objects")]
    public Transform firepoint;
    public Transform Ultfirepoint;
    public GameObject bulletprefab;
    public GameObject Ultprefab;
    [Header("H. Attributes")]
    public float TimeBetweenHealth = 5;
    float timer;
    public GameObject HealingVfx;
    [Header("Cooldowns")]
    public Image imageHealthCooldown;
    public Image imageBlastCooldown;

    bool blasting;
    bool healingCast;
    bool isCooldownH;
    bool isCooldownB;

    Animator anim;
    Transform Self;

    void Awake(){
        Self = transform;
        anim = GetComponent<Animator>();}

    void Update(){
        fireCountdown -= Time.deltaTime;
        timer += Time.deltaTime;
        BlastTimer += Time.deltaTime;
        if ((Input.GetButtonDown("LFire") && Input.GetButton("LHeal") || (Input.GetButtonDown("LHeal") && Input.GetButton("LFire")) && BlastTimer >= TimeBetweenBlast)){
            if (healingCast == false){
                blasting = true;
                ShootingBlastAnimation();
                BlastTimer = 0f;
                isCooldownB = true;}         
        }
        BlastCooldown();
        if (Input.GetButton("LFire") && fireCountdown <= 0f){
            if (blasting == false){
                ShootingAnimation();
                fireCountdown = 1f / fireRate;}
            else{
                return;}}
        else if (Input.GetButton("LHeal") && timer >= TimeBetweenHealth){
            if (blasting == false){
                HealingAnimation();
                timer = 0f;
                isCooldownH = true;
                healingCast = true;}}
        HealingCooldown();
    }
        void HealingCooldown(){
        if (isCooldownH == true){
            imageHealthCooldown.fillAmount += 1 / TimeBetweenHealth * Time.deltaTime;
            if (imageHealthCooldown.fillAmount >= 1){
                imageHealthCooldown.fillAmount = 0;
                isCooldownH = false;}}}
        void BlastCooldown(){
        if (isCooldownB == true){
            imageBlastCooldown.fillAmount += 1 / TimeBetweenBlast * Time.deltaTime;
            if (imageBlastCooldown.fillAmount >= 1){
                imageBlastCooldown.fillAmount = 0;
                isCooldownB = false;
            }}}
        void Shoot(){
        Instantiate(bulletprefab, firepoint.position, firepoint.rotation);}
        void ShootBlast(){
        Instantiate(Ultprefab, Ultfirepoint.position, Ultfirepoint.rotation);}
    void ShootingAnimation(){
        anim.SetTrigger("Shooting");}

    void ShootingBlastAnimation(){
        anim.SetTrigger("Blast");}

    void HealingAnimation(){
        Instantiate(HealingVfx, Self.position, Self.rotation);
        anim.SetTrigger("Healing");}

    void ShootBlastFinished(){
        blasting = false;}

    void HealingFinished(){
        healingCast = false;}
}
