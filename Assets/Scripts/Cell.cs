using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject onCell = null;
    public bool isBlocked { get; set; }
    public GameObject indic;
    public int X, Y;
    //object avec technique bougie pour sur l'unit select, les case valid de dep, les case ou ennemi dans la range

    //value for pathfinding
    [System.NonSerialized] public bool visited;
    [System.NonSerialized] public List<Cell> neighbors;
    [System.NonSerialized] public Cell parent;
    [System.NonSerialized] public Node<Cell> node;

    private void Start()
    {
        Vector3 position = transform.position;
        X = (int)position.x;
        Y = (int)position.z;
    }

    public void SetBlocker(bool blocker)
    {
        isBlocked = blocker;
        //indic.SetActive(blocker);
    }

    public void SetIndic(bool active, Material mat)
    {
        indic.SetActive(active);
        indic.GetComponent<ParticleSystemRenderer>().material = mat;
    }

    public void SetMaterial(Material mat) //utile pour des test
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    private void OnMouseDown()
    {
        if (onCell != null && GameManager.Instance.aliensTurnEnd == 0)
        {
            if (MouseManager.mouseState == MouseManager.MouseState.select)
                MouseManager.Instance.Select(onCell);

            if (MouseManager.Instance.select != null && 
                onCell.GetComponent<EnemyUnit>() != null && 
                MouseManager.mouseState == MouseManager.MouseState.shoot &&
                indic.activeSelf)
            {
                MouseManager.Instance.Shoot(onCell);
                MouseManager.Instance.select.GetComponent<UnitMove>().CleanInRange();
            }
        }
        if(MouseManager.Instance.select != null)
            if (MouseManager.Instance.select.GetComponent<AllyUnit>() != null)
                if (MouseManager.Instance.select.GetComponent<AllyUnit>().actionPoint > 0 &&
                    MouseManager.mouseState == MouseManager.MouseState.move &&
                    onCell == null &&
                    indic.activeSelf)
                {
                    MouseManager.Instance.select.GetComponent<UnitMove>().GoTo(this);
                }
    }
}
