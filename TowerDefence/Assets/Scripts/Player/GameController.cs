using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int HP = 20;
    int Gold = 1;
    int killEnemy = 0;
    int currentWave = 0;
    bool theEnd = false;
    private UnityAction waveListener;

    Text HPText;
    Text GoldText;
    Text WaveText;
    Text GameOverText;

    void Awake()
    {
        waveListener = new UnityAction(WaveFunction);
    }

    void Start ()
    {
        GameObject.Find("House").GetComponent<House>().DestroyedHouse += (d) => DestroyedHouse(d);
        for(int i = 0; i < transform.childCount; ++i)
        {
            var tower = transform.GetChild(i).GetComponent<Tower>();
            tower.CanBuy = (g) => g <= Gold;
            tower.BuyStat = (g) => { Gold -= g; ChangeGoldText(); };
            tower.AddGold = (g) => { Gold += g; killEnemy++; ChangeGoldText(); };
        }
        //Установка всех текстов
        Transform texts = GameObject.Find("GameCanvas").transform.GetChild(0);
        HPText = texts.GetChild(0).GetChild(1).GetComponent<Text>();
        GoldText = texts.GetChild(1).GetChild(1).GetComponent<Text>();
        WaveText = texts.GetChild(2).GetChild(0).GetComponent<Text>();
        GameOverText = texts.parent.GetChild(1).GetChild(1).GetComponent<Text>();
    }

    private void WaveFunction()
    {
        currentWave++;
        ChangeWaveTExt();
    }

    void DestroyedHouse(int damage)
    {
        HP -= damage;
        
        if (!theEnd && HP <= 0)
        {
            theEnd = true;
            GameOverText.text = "Wave " + (currentWave - 1).ToString() + "\nKill " + killEnemy.ToString();
            GameOverText.transform.parent.gameObject.SetActive(true);
            HP = 0;
        }
        else if(HP <= 0)
        {
            HP = 0;
        }
        ChangeHPText();
    }

    void OnEnable()
    {
        EventManager.StartListening("wave", waveListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("wave", waveListener);
    }

    void ChangeHPText()
    {
        HPText.text = HP.ToString();
    }

    void ChangeGoldText()
    {
        GoldText.text = Gold.ToString();
    }

    void ChangeWaveTExt()
    {
        WaveText.text = "Wave " + currentWave.ToString(); 
    }
}
