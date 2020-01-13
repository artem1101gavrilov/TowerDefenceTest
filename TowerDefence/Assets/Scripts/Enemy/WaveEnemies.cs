using System.Collections.Generic;
using UnityEngine;

public class WaveEnemies : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    List<Vector3> Path = new List<Vector3>();
    int currentWave = 0;
    int x;
    float timeNewWave = 2;
    IEnemy AllEnemyState;
    List<Enemy> Enemies = new List<Enemy>();

    private void Start()
    {
        AllEnemyState = new EnemyStats() { Health = 1, Attack = 1, Gold = 1 };
        var PathParent = GameObject.Find("Path").transform;
        for(int i = 0; i < PathParent.childCount; ++i)
        {
            Path.Add(PathParent.GetChild(i).position);
        }
        Invoke("NewWave", timeNewWave);
    }

    void NewWave()
    {
        currentWave++;
        EventManager.TriggerEvent("wave");
        x = Random.Range(1, 10) + currentWave;
        for (int i = 0; i < x; ++i)
        {
            Invoke("NewEnemy", i+1);
        }
    }

    void NewEnemy()
    {
        var enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity, transform).GetComponent<Enemy>();
        enemy.SetPath(Path);
        enemy.SetEnemyStats(AllEnemyState);
        enemy.SetWaveController(this);
        Enemies.Add(enemy);
    }

    public void DestroyEnemy(Enemy enemy)
    {
        //Проверка наличия на карте врагов в волне
        Enemies.Remove(enemy);
        if (Enemies.Count == 0)
        {
            RandomEnemyState();
            Invoke("NewWave", timeNewWave);
        }
    }

    void RandomEnemyState()
    {
        bool[] addState = new bool[3];
        do
        {
            addState[0] = Random.Range(0, 2) == 1 ? true : false;
            addState[1] = Random.Range(0, 2) == 1 ? true : false;
            addState[2] = Random.Range(0, 2) == 1 ? true : false;
        } while (!addState[0] && !addState[1] && !addState[2]);
        if (addState[0]) AllEnemyState.Health++;
        if (addState[1]) AllEnemyState.Attack++;
        if (addState[2]) AllEnemyState.Gold++;
    }
}

struct EnemyStats : IEnemy
{
    public int Health{ get; set; }
    public int Attack { get; set; }
    public int Gold { get; set; }
}