using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject select;

    public static MouseManager Instance { get; private set; }

    public enum MouseState
    {
        select,
        move,
        shoot
    }
    public static MouseState mouseState { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void ChangeMouseState(MouseState state)
    {
        mouseState = state;
        switch (mouseState)
        {
            case MouseState.select:
                if(select != null)
                    if (select.GetComponent<UnitMove>() != null)
                    {
                        select.GetComponent<UnitMove>().CleanInRange();
                        select.GetComponent<UnitMove>().CleanFreeCell();
                    }
                break;
                
            case MouseState.move:
                if (select.GetComponent<UnitMove>() != null)
                {
                    select.GetComponent<UnitMove>().CleanInRange();
                    select.GetComponent<UnitMove>().FreeCell();
                }
                break;
            case MouseState.shoot:
                if (select.GetComponent<UnitMove>() != null)
                {
                    select.GetComponent<UnitMove>().CleanFreeCell();
                    select.GetComponent<UnitMove>().InRange();
                }
                break;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (select != null)
            {
                if (select.GetComponent<UnitMove>() != null)
                {
                    select.GetComponent<UnitMove>().CleanFreeCell();
                    select.GetComponent<UnitMove>().CleanInRange();
                }
                if (select != null) select = null;
            }
            ChangeMouseState(MouseState.select);
            UIManager.Instance.UnSelect();
        }
    }
    //un clic droit remet en mode select et forget last select (update maybe)

    public void Shoot(GameObject target)
    {
        if (select.GetComponent<AllyUnit>().actionPoint > 0)
        {
            target.GetComponent<EnemyUnit>().ChangeHealth(select.GetComponent<AllyUnit>().attDamage);
            select.GetComponent<AllyUnit>().actionPoint--;
            ChangeMouseState(MouseState.select);
        }
    }

    public void Select(GameObject unit)
    {
        select = unit;
        if(unit.GetComponent<AllyUnit>() != null) unit.GetComponent<AllyUnit>().DisplayInfo();
        if (unit.GetComponent<EnemyUnit>() != null) unit.GetComponent<EnemyUnit>().DisplayInfo();
    }
}
