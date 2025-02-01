using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    //если произошло условие перехода, принимаем это состяние
    [SerializeField] private State _targetState;

    protected Player Target { get; private set; }

    public State TargetState => _targetState;

    //необходимость для перехода
    public bool NeedTransit { get; protected set; }

    public void Init(Player target)
    {
        //Проверяльщик
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
