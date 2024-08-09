using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MoveState : IStates
{
    StatesManager m_manager;
    Transform m_Transform;
    float m_speed;
    Vector3 startPos;
    Vector3 currPos;
    public MoveState(EnemyBehaviour enemy, StatesManager manager)
    {
        m_speed = enemy.speed;
        m_Transform = enemy.transform;
        m_manager = manager;
    }

    public void OnEnter()
    {
        startPos = m_Transform.position;
    }

    public void OnUpdate()
    {
        m_Transform.position += m_Transform.forward * m_speed * Time.deltaTime;
        currPos = m_Transform.position;
        if(Vector3.Distance(currPos, startPos) > 5)
        {
            m_manager.ChangeState(EnemyStates.IDLE);
        }
    }

    public void OnExit()
    {   
        
    }
}

public class IdleState : IStates
{
    StatesManager m_manager;
    float startTimer;
    float m_IdleTime;
    public void OnEnter()
    {
        m_IdleTime = startTimer;
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        m_IdleTime -= Time.deltaTime;
        if(m_IdleTime <= 0)
        {
            //m_manager.ChangeState(EnemyStates.MOVE);
        }
    }

    public IdleState(float IdleTime, StatesManager manager)
    {
        m_manager = manager;
        startTimer = IdleTime;
    }
}

public class AttackState : IStates
{
    StatesManager m_manager;
    IDetector m_detector;
    Transform m_turret;

    Action m_shooting;

    LifeSystem m_lifeSystem;

    public void OnEnter()
    {
        //throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        if(m_lifeSystem.HP < 1)
        {
            m_manager.ChangeState(EnemyStates.IDLE);
        }
        else if(m_detector.Object() != null)
        {
            //m_turret.LookAt(m_detector.Object().transform.position);
            Vector3 targetDirection = (m_detector.Object().transform.position - m_turret.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            m_turret.rotation = Quaternion.Slerp(m_turret.rotation, targetRotation, Time.deltaTime * 0.5f);
            m_shooting();
        }
    }

    public void OnExit()
    {
        //throw new System.NotImplementedException();
    }


    public AttackState(IDetector detector, Transform turret, Action Shoot, StatesManager manager, LifeSystem hp)
    {
        m_manager = manager;
        m_detector = detector;
        m_turret = turret;
        m_shooting = Shoot;
        m_lifeSystem = hp;
    }

}


