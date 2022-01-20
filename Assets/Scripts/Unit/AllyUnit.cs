using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : MonoBehaviour
{
    public UnitScriptableObject astro;
    public int HP;
    int HPMax;
    [System.NonSerialized] public int attDamage;
    int attRange;
    public int actionPoint;
    public int actionPointMax;
    
    void Start()
    {
        name = astro.Name;
        HP = astro.HP;
        HPMax = astro.HP;
        attDamage = astro.attDamage;
        attRange = astro.attRange;
        actionPoint = astro.actionPoint;
        actionPointMax = astro.actionPoint;
        
    }
    public void DisplayInfo()
    {
        UIManager.Instance.Select(name, HP, HPMax, actionPoint, actionPointMax, attRange, attDamage, astro.moveRange);
        UIManager.Instance.ActiveButon();
    }
    public void NewTurn()
    {
        actionPoint = actionPointMax;
        if (GameManager.Instance.leftBorder > transform.position.x)
            Destroy(gameObject);
    }
    public void ChangeHealth(int damage)
    {
        HP -= damage;
        if(HP < 1)
        {
            UIManager.Instance.UnSelect();
            Destroy(gameObject); //pas propre faire un truc pour le clear de la liste du Gamemanager
        }
        DisplayInfo();
    }
}
