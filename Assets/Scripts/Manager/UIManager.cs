using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Unitname;
    public Text PV;
    public Text action;
    public Text attack;
    public Text move;
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        
    }
    public void Select(string name, int HP, int HPMax, int PA, int PAMax, int att, int mov)
    {
        Unitname.text = name;
        PV.text = HP + " / " + HPMax;
        action.text = PA + " / " + PAMax;
        attack.text = "" + att;
        move.text = "" + mov;
    }
    public void ButtonNextTurn()
    {
        GameManager.Instance.AITurn();
    }
    public void ButtonMove()
    {
        if(MouseManager.Instance.select.GetComponent<AllyUnit>() != null)
        {
            MouseManager.Instance.select.GetComponent<UnitMove>().FreeCell();
            MouseManager.Instance.ChangeMouseState(MouseManager.MouseState.move);
        }
    }
    public void ButtonAtt()
    {
        if (MouseManager.Instance.select.GetComponent<UnitMove>() != null)
        {
            MouseManager.Instance.select.GetComponent<UnitMove>().InRange();
            MouseManager.Instance.ChangeMouseState(MouseManager.MouseState.shoot);
        }
    }
}
