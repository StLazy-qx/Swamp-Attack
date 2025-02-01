using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    //состояния будем отдельно создавать из абстракции
    //несколько вариантов переходов
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; set; }

    public void Enter(Player target)
    {
        if (enabled == false)
        {
            Target = target;
            enabled = true;

            //при старте включавем себя и включаем все наши переходы состяний
            foreach (var transition in _transitions)
            {
                //переходы проверяет какое состяние должен принять объект, какие сейчас условия
                //если что то сделано , то иди в такое состояние
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            //если переход готов, то возвращаем следующее состяние
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }
}
