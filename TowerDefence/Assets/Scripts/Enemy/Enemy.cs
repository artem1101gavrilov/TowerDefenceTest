using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IEnemy
{
    float speed = 5.0f;
    int maxHealth;
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Gold { get; set; }
    public bool IsAlive { get; set; }
    List<Vector3> Path;
    Vector3 target;
    SpriteRenderer spriteRenderer;
    WaveEnemies waveEnemies;
    Image HPBar;

    private void Start()
    {
        HPBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        IsAlive = true;
    }

    void Update ()
    {
        Move();
    }

    public void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            NewTarget();
        }
    }

    private void NewTarget(bool remove = true)
    {
        if(remove) Path.Remove(target);
        target = Path[0];
        if (target.x > transform.position.x) spriteRenderer.flipX = false;
        else spriteRenderer.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            collision.GetComponent<House>().Damage(Attack);
            DestroyEnemy();
        }
    }

    public void SetPath(List<Vector3> NewPath)
    {
        Path = new List<Vector3>(NewPath);
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        NewTarget(false);
    }

    public void SetEnemyStats(IEnemy enemy)
    {
        maxHealth = Health = enemy.Health;
        Attack = enemy.Attack;
        Gold = enemy.Gold;
    }

    public void SetWaveController(WaveEnemies waveEnemies)
    {
        this.waveEnemies = waveEnemies;
    }

    //Вернем деньги, если убили
    public int Damage(int damage)
    {
        Health -= damage;
        HPBar.fillAmount = (float)Health / maxHealth;
        if (Health <= 0)
        {
            DestroyEnemy();
            return Gold;
        }
        return 0;
    }

    void DestroyEnemy()
    {
        IsAlive = false;
        waveEnemies.DestroyEnemy();
        Destroy(gameObject);
    }

}
