using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyStateMachine : MonoBehaviour
{
    //отвечает за состояние объекта
    [SerializeField] private State _firstState;

    private Player _target;
    private State _currentState;

    public State Current => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        //сначала проверяем, если не null, какое будет следующее наше состояние
        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        //все начинается с ресета
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();
        //выходим из этого состояния

        _currentState = nextState;

        //входим в следующее состояние если оно есть
        if (_currentState != null)
            _currentState.Enter(_target);
    }
}
