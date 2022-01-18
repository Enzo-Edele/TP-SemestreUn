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
                break;
            case MouseState.move:
                break;
            case MouseState.shoot:
                break;
        }
    }

    public void Shoot(GameObject target)
    {
        target.GetComponent<EnemyUnit>().HP -= select.GetComponent<AllyUnit>().attDamage; //mettre un truc pour la range genre check le mat
    }

    public void Select(GameObject unit)
    {
        select = unit;
        if(unit.GetComponent<AllyUnit>() != null) unit.GetComponent<AllyUnit>().DisplayInfo();
    }
}
