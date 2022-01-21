using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public GridPathFinding grid;
    void Start()
    {
        grid.grid[(int)transform.position.x, (int)transform.position.z].SetBlocker(true);
    }
}
