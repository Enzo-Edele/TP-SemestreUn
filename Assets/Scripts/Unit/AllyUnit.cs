using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : MonoBehaviour
{
    public UnitScriptableObject astro;
    [System.NonSerialized] public int HP;
    int HPMax;
    [System.NonSerialized] public int attDamage;
    int attRange;
    int actionPoint;
    int actionPointMax;
    
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
    void Update()
    {
        
    }
    public void DisplayInfo()
    {
        UIManager.Instance.Select(name, HP, HPMax, actionPoint, actionPointMax, attDamage, astro.moveRange);
    }
    public void NewTurn()
    {
        actionPoint = actionPointMax;
        if (GameManager.Instance.leftBorder > transform.position.x)
            Destroy(gameObject);
    }
    //pour marquer les ennemis : faire un if et ne pas faire des qu'une coord est out
}
