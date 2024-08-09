using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehabiour : MonoBehaviour
{
    public Transform target;
    public Transform m_turret;
    IDetector m_detector;
    public Ammo m_ammo;
    public float ShootingTimer = 0f;
    public float fireRate = 0.4f;
    public GameObject barrelPoint;
    AudioSystem audioSystem;
    public AudioClip[] audios;
    public LayerMask playerMask;
    void Start()
    {
        m_detector = GetComponentInChildren<IDetector>();
        audioSystem = new AudioSystem(GetComponent<AudioSource>(), audios);
    }

    void Update()
    {
        if(m_detector.Object() != null)
        {
            Vector3 targetDirection = (m_detector.Object().transform.position - m_turret.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            m_turret.rotation = Quaternion.Slerp(m_turret.rotation, targetRotation, Time.deltaTime * 0.5f);
            Fire();
        }
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
                    var bullet = Instantiate(m_ammo, barrelPoint.transform.position, barrelPoint.transform.rotation);
                    bullet.Fire(barrelPoint.transform.forward);
                    ShootingTimer = 0;
                }
            }
        }

    }
}
