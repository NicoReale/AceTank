using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
    // Start is called before the first frame update

    public JoyController _moveJoystick;
    public JoyControllerAim _aimJoystick;
    public GameObject _turret;
    public GameObject _barrel;
    public float _speed = 3;
    [SerializeField] float _aimSpeed = 0;

    public float friction = 2f;

    Rigidbody _rigidbody;

    Vector3 vel;

    Vector3 rotation;

    public void ApplyForce(Vector3 force)
    {
        vel += force;
    }

    void ApplyFriction()
    {
        vel = vel * (1f - Time.deltaTime * friction);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyFriction();
        _rigidbody.MovePosition(transform.position + vel * Time.deltaTime);
        if(Mathf.Abs(_moveJoystick.GetMovementInput().y) >= .05f || Mathf.Abs(_moveJoystick.GetMovementInput().y) <= -.05f)
        {
            ApplyForce(transform.forward * (-_moveJoystick.GetMovementInput().y - .05f) * _speed);
        }
        if (Mathf.Abs(_moveJoystick.GetMovementInput().x) >= .05f || Mathf.Abs(_moveJoystick.GetMovementInput().x) <= -.05f)
        {
            rotation = new Vector3(0, _moveJoystick.GetMovementInput().x) * (_speed * 7) * Time.deltaTime;
            _rigidbody.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
        }

        if (Mathf.Abs(_aimJoystick.Horizontal) >= .1f || Mathf.Abs(_aimJoystick.Horizontal) >= .1f)
        {
            _turret.transform.localEulerAngles += new Vector3(0, _aimJoystick.Horizontal) / 1.5f * _aimSpeed;
        }
    }
}
