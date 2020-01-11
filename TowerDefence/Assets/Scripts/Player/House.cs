using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    int HP;
    public Action DestroyedHouse;

    private void Start()
    {
        HP = 20;
    }

    public void Damage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            DestroyedHouse();
        }
    }
}
