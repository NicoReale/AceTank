using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVarBehaviour : LifeSystem
{
    public float speed = 5f;
    public float obstacleDistance = 1f;
    public LayerMask obstacleLayer;
    public float rotationSpeed = 10f;
    public float playerAvoidDistance = 3f;
    public float playerAvoidAngle = 90f;

    public float ShootingTimer;
    public LayerMask playerMask;
    public GameObject barrelPoint;
    public Ammo ammoType;

    public GameObject objectBody;

    public Vector3 direction;
    private RaycastHit hit;
    private Quaternion targetRotation;

    Rigidbody rb;

    public IDetector detector;

    Color originalColor;

    public Transform m_turret;

    float avoidTimer = 3;
    float avoid = 0;

    public float aimSpeed;
    GameObject target;

    void Start()
    {
        targetRotation = Quaternion.identity;
        detector = GetComponentInChildren<IDetector>();
        originalColor = GetComponentInChildren<Renderer>().material.color;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (detector.Object() != null)
        {
            target = detector.Object();
        }
        if(target != null)
        {
            Move(target.transform.position);
            Fire(); 
        }

    }

    public void Move(Vector3 targetPosition)
    {
        //direction = (targetPosition - transform.position).normalized;

        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDistance, obstacleLayer))
        {
            Vector3 normal = hit.normal;
            direction = Vector3.Reflect(direction, normal);
        }
        else if (Vector3.Distance(transform.position, targetPosition) < playerAvoidDistance)
        {
            if (avoid >= 2)
                direction = -direction;
            StartAvoid();
        }
        else
        {
            avoid += Time.deltaTime;
            if(avoid > avoidTimer)
                direction = (targetPosition - transform.position).normalized;          
        }
        Vector3 targetDirection = (targetPosition - m_turret.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, transform.up);
        m_turret.rotation = Quaternion.Slerp(m_turret.rotation, targetRotation, Time.deltaTime * aimSpeed);

        targetRotation = Quaternion.LookRotation(direction);
        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }
    public void Fire()
    {

        ShootingTimer += Time.deltaTime;
        if (Physics.Raycast(barrelPoint.transform.position, barrelPoint.transform.forward + Random.insideUnitSphere * 0.075f, out RaycastHit hit, 100, playerMask))
        {
            if (hit.transform.GetComponent<PlayerBehaviour>())
            {
                if (ShootingTimer > 1.5f)
                {
                    var bullet = Instantiate(ammoType, barrelPoint.transform.position, barrelPoint.transform.rotation);
                    bullet.Fire(barrelPoint.transform.forward);
                    ShootingTimer = 0;
                }
            }
        }
    }

    void StartAvoid()
    {
        avoid = 0;
    }

    public override void DamageFeedback()
    {
        //audioSource.clip = audios[0];
        /*if (gameObject != null)
            audioSource.Play();*/
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
        /*audioSource.clip = audios[1];
        audioSource.Play();*/
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward *obstacleDistance);
    }
}
