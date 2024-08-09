using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesManager
{
    public IStates _currentState;

    Dictionary<EnemyStates, IStates> _states = new Dictionary<EnemyStates, IStates>();

    public void AddState(EnemyStates stateName, IStates stateToAdd)
    {
        if(_states.ContainsKey(stateName))
        {
            return;
        }
        _states.Add(stateName, stateToAdd);
    }

    public void Update()
    {
        if(_currentState != null)
            _currentState.OnUpdate();
    }

    public void ChangeState(EnemyStates nextState)
    {
        if (_currentState == _states[nextState]) return;
        if (!_states.ContainsKey(nextState)) return;
        _currentState?.OnExit();
        _currentState = _states[nextState];
        _currentState.OnEnter();
        
    }
}
