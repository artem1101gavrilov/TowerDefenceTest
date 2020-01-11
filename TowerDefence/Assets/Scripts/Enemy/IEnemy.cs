using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    int Health { get; set; }
    int Attack { get; set; }
    int Gold { get; set; }

    void SetEnemyStats(IEnemy enemy);
}
