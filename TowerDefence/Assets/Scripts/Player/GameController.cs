using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    int Gold = 10;

	void Start ()
    {
        GameObject.Find("House").GetComponent<House>().DestroyedHouse += () => DestroyedHouse();
        for(int i = 0; i < transform.childCount; ++i)
        {
            var tower = transform.GetChild(i).GetComponent<Tower>();
            tower.CanBuy = (g) => g <= Gold;
            tower.BuyStat = (g) => Gold -= g;
            tower.AddGold = (g) => Gold += g;
        }
    }

    //Уничтожен дом
    void DestroyedHouse()
    {

    }
}
