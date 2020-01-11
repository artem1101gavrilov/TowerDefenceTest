using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    float speed = 1.0f;
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Gold { get; set; }
    List<Vector3> Path;
    Vector3 target;
    SpriteRenderer spriteRenderer;

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
        
    }

    public void SetPath(List<Vector3> NewPath)
    {
        Path = new List<Vector3>(NewPath);
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        NewTarget(false);
    }

    public void SetEnemyStats(IEnemy enemy)
    {
        Health = enemy.Health;
        Attack = enemy.Attack;
        Gold = enemy.Gold;
    }
}
