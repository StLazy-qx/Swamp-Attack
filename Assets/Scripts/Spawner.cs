using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    //текущая волна
    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    //сколько врагов было уже созданно
    private int _spawned;

    public event UnityAction AllEnemySpawned;
    //так же как и с жизнями, передаем текущее значение и максимальное
    public event UnityAction<int, int> EnemyCountChanged;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        //если волны закончились
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        //если время станет больше задержки
        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
            //при спавне врага создаем событие на подсчет врагов
            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        //обнуление волны, уоторая в данный момент идет
        if (_currentWave.Count <= _spawned)
        {
            //подсчет спауна всех врагов на волне
            if (_waves.Count > _currentWaveNumber + 1)
            {
                //на него должны среагировать
                AllEnemySpawned?.Invoke();
            }

            _currentWave = null;
        }
    }

    public void NextWave()
    {
        //устанавливаем следующую волну под начажатие на кнопку
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position,
            _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);

        //подписываем событие
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        //установить определенную волну
        _currentWave = _waves[index];
        //настройка 0 значения на слайдере прогресса спавна врагов
        EnemyCountChanged?.Invoke(0, 1);
    }

    //обрабатываем событие что енеми умер
    // сюда приходит enemy, на который мы можем подписаться или отписаться
    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        //игрок получает деньги в награду
        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}