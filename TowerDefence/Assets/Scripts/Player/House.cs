using System;
using UnityEngine;

public class House : MonoBehaviour
{
    
    public Action<int> DestroyedHouse;
    public void Damage(int damage)
    {
        DestroyedHouse(damage);
    }
}
