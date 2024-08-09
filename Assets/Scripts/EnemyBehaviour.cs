using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyStates
{
    MOVE,
    IDLE,
    ATTACK
}
public class EnemyBehaviour : LifeSystem
{

    Color originalColor;
    StatesManager statesManager;
    //MoveState moveState;
    IdleState idleState;
    AttackState attackState;

    AudioSource audioSource;

    public AudioClip[] audios;

    IDetector detector;

    float ShootingTimer = 0;
    bool damageTaken = false;

    public LayerMask playerMask;

    public Ammo ammoType;
    public GameObject barrelPoint;
    public Transform turret;

    public GameObject objectBody;


    public float speed = 3f;
    public float idleTime = 2f;



    public void Fire()
    {
        ShootingTimer += Time.deltaTime;
        if(Physics.Raycast(barrelPoint.transform.position, barrelPoint.transform.forward + Random.insideUnitSphere * 0.075f, out RaycastHit hit, 100, playerMask))
        {
            if(hit.transform.GetComponent<PlayerBehaviour>())
            {
                if(ShootingTimer > 1.5f)
                {
                    var bullet = Instantiate(ammoType, barrelPoint.transform.position, barrelPoint.transform.rotation);
                    bullet.Fire(barrelPoint.transform.forward);
                    ShootingTimer = 0;
                }
            }
        }
    }
    private void Awake()
    {
        originalColor = GetComponentInChildren<Renderer>().material.color;
        statesManager = new StatesManager();
        detector = GetComponentInChildren<IDetector>();
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
        idleState = new IdleState(idleTime, statesManager);
        attackState = new AttackState(detector, turret, Fire, statesManager, this);

        statesManager.AddState(EnemyStates.IDLE, idleState);
        statesManager.AddState(EnemyStates.ATTACK, attackState);


        statesManager.ChangeState(EnemyStates.IDLE);
    }

    void Update()
    {
        statesManager.Update();
        if (detector.IsDetected() || damageTaken)
        {
            statesManager.ChangeState(EnemyStates.ATTACK);
        }
    }

    public override void DamageFeedback()
    {
        audioSource.clip = audios[0];
        damageTaken = true;
        if(gameObject != null)
            audioSource.Play();
        GetComponentInChildren<PlayerDetector>().gameObject.GetComponent<CapsuleCollider>().radius *= 5;
        StartCoroutine(ColorChange());
    }

    public override IEnumerator ColorChange()
    {
        GetComponentInChildren<Renderer>().material.color = Color.blue;
        yield return new WaitForSeconds(.2f);
        GetComponentInChildren<Renderer>().material.color = originalColor;

    }

    public override IEnumerator onDeath()
    {
        objectBody.SetActive(false);
        audioSource.clip = audios[1];
        audioSource.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
