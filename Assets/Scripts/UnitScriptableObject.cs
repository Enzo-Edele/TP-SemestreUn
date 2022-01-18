using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit ScriptableObject")]

public class UnitScriptableObject : ScriptableObject
{
    public string Name;

    public int HP;
    public int attDamage;
    public int attRange;
    public int moveRange;
    public int actionPoint;
}
