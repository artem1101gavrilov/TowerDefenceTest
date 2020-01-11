using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    public float Reloading { get; set; }
    public int Attack { get; set; }
    float timer;
    List<GameObject> Enemies = new List<GameObject>();
    Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
        Attack = 1;
        Reloading = 2;
        timer = 2;
    }
	
	void Update ()
    {
	    if(timer < Reloading)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Shoot();
        }
	}

    void Shoot()
    {
        while(Enemies.Count > 0)
        {
            if(Enemies[0] != null)
            {
                timer = 0;
                Enemies[0].GetComponent<Enemy>().Damage(Attack);
                animator.Play("shoot");
                break;
            }
            else
            {
                Enemies.RemoveAt(0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            Enemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            Enemies.Remove(collision.gameObject);
        }
    }
}
