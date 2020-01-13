using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tower : MonoBehaviour, ITower
{
    int LevelReloading;
    int LevelAttack;
    public float Reloading { get; set; }
    public int Attack { get; set; }
    float timer;
    List<GameObject> Enemies = new List<GameObject>();
    Animator animator;

    public System.Predicate<int> CanBuy;
    public System.Action<int> BuyStat;
    public System.Action<int> AddGold;
	
	Image ButtonReloading;
	Image ButtonAttack;
	TextMeshProUGUI TextReloading;
	TextMeshProUGUI TextAttack;

    void Start ()
    {
        animator = GetComponent<Animator>();
        Attack = 1;
        Reloading = 2;
        timer = 2;
        LevelReloading = 1;
        LevelAttack = 1;
		ButtonReloading = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Image>();
		ButtonAttack = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
		TextReloading = ButtonReloading.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
		TextAttack = ButtonAttack.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
                var g = Enemies[0].GetComponent<Enemy>().Damage(Attack);
                if (g > 0) AddGold(g);
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

    void SetColorButtons()
    {
        ButtonReloading.color = new Color(1, CanBuy((int)Mathf.Pow(2, LevelReloading)) ? 1 : 0.4f, 0.4f, 1);
        ButtonAttack.color = new Color(1, CanBuy((int)Mathf.Pow(2, LevelAttack)) ? 1 : 0.4f, 0.4f, 1);
		TextReloading.text = ((int)Mathf.Pow(2, LevelReloading)).ToString();
		TextAttack.text = ((int)Mathf.Pow(2, LevelAttack)).ToString();
    }

    private void OnMouseDown()
    {
        //Открываем канвас с показанием уровня и стоимостью следующей покупки  
        transform.GetChild(0).gameObject.SetActive(true);
        //Красим кнопки покупки в цвет в зависимости от количества имеющихся денег.
        SetColorButtons();
    }

    private void OnMouseExit()
    {
        //Закрываем канвас
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void BuyAttack()
    {
        if(CanBuy((int)Mathf.Pow(2, LevelAttack)))
        {
            BuyStat((int)Mathf.Pow(2, LevelAttack));
            Attack++;
            LevelAttack++;
            SetColorButtons();
        }
    }

    public void BuyReloading()
    {
        if (CanBuy((int)Mathf.Pow(2, LevelReloading)))
        {
            BuyStat((int)Mathf.Pow(2, LevelReloading));
            Reloading -= 0.1f;
            LevelReloading++;
            SetColorButtons();
        }
    }
}
