using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public GridPathFinding grid;
    void Start()
    {
        Vector3 position = transform.position;
        grid.grid[(int)position.x, (int)position.z].SetBlocker(true);
    }
}
