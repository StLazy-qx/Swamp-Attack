using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    //хранит здоровье и награду за врага и отвечает за прием урона и смерть
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    private Player _target;

    public int Reward => _reward;
    public Player Target => _target;

    public event UnityAction<Enemy> Dying;

    public void Init(Player target)
    {
        //враг при спавне знает о игроке
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            //подключаем выполнение события при выполнении условия
            //this - значит передаем себя, объект, которому
            //в данный момент принадлежит компонент Enemy
            Dying?.Invoke(this);
            Destroy(gameObject);
        }
    }
}