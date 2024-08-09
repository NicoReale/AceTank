using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : LifeSystem
{
    public Ammo ammoType;
    public GameObject barrelPoint;
    float fireTimer = 0;
    public float damageModifier = 0;

    public float speedModifier = 0;
    Color matColor;

    public AudioClip[] audios;
    public GameObject player;
    public GameObject MovementCanvas;
    public GameObject ObjectiveCanvas;
    public GameObject HealthSlider;
    AudioSystem audioSystem;

    PlayerUIHandler playerUIHandler;

    public bool god = false;


    private void Start()
    {
        audioSystem = new AudioSystem(GetComponent<AudioSource>(), audios); 
        playerUIHandler = new PlayerUIHandler(HealthSlider.GetComponent<UnityEngine.UI.Image>());
        matColor = GetComponentInChildren<Renderer>().material.color;
        changeStats();
        GetComponent<PlayerMovement>()._speed += speedModifier;
        UIUpdate += HealingUIUpdate;
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;
    }

    public void Fire()
    {
        if(fireTimer > 1.2f)
        {
            GameManager.instance.AddShot();
            var bullet = Instantiate(ammoType, barrelPoint.transform.position, barrelPoint.transform.rotation);
            bullet.setDamage(damageModifier);
            bullet.Fire(barrelPoint.transform.forward);
            fireTimer = 0;
            audioSystem.PlayAudio(2);
        }
    }
    public void HealingUIUpdate()
    {
        playerUIHandler.UpdateSlider(HP);
    }
    public override void DamageFeedback()
    {
        playerUIHandler.UpdateSlider(HP);
        audioSystem.PlayAudio(0);
        StartCoroutine(ColorChange());
    }
    public override void ReceiveDamage(float damage)
    {
        if (!god)
            base.ReceiveDamage(damage);
    }
    public override IEnumerator ColorChange()
    {
        GetComponentInChildren<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<Renderer>().material.color = matColor;
    }

    public void changeStats()
    {
        Debug.Log(PlayerPrefs.GetFloat("SpeedMult"));
        speedModifier = PlayerPrefs.GetFloat("SpeedMult");
        damageModifier = PlayerPrefs.GetFloat("DamageMult");
        //GameManager.instance.playerStats[0];
        //GameManager.instance.playerStats[1];
    }

    public override IEnumerator onDeath()
    {
        player.SetActive(false);
        audioSystem.PlayAudio(1);
        MovementCanvas.SetActive(false);
        ObjectiveCanvas.SetActive(false);
        yield return new WaitForSeconds(1);
        ScenesManager.Instance.EndGame();
    }

}
