using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    //value for pathfinding
    public GridPathFinding grid;
    Cell currentCell;
    List<Cell> pathList;
    //value pour aligner dans la direction du mouvement
    Vector3 posA = new Vector3();
    Vector3 posB = new Vector3();

    public Material enemy, acces, ally;
    public List<GameObject> animated;

    public UnitScriptableObject unit;

    void Start()
    {
        Vector3 position = transform.position;
        currentCell = grid.grid[(int)position.x, (int)position.z];
        grid.grid[(int)position.x, (int)position.z].onCell = this.gameObject;
        grid.grid[(int)position.x, (int)position.z].SetBlocker(true);
        pathList = new List<Cell>();
    }
    void Update()
    {
        if (pathList.Count > 0)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pathList[0].transform.position,
                Time.deltaTime * 2);
            if (transform.position == pathList[0].transform.position)
            {
                currentCell = pathList[0];
                pathList.RemoveAt(0);
            }
        }
        else if(!currentCell.isBlocked)
        {
            currentCell.SetBlocker(true);
            Animator anim;
            for (int i = 0; i < animated.Count; i++)
            {
                anim = animated[i].GetComponent<Animator>();
                anim.SetTrigger("WalkCycle");
            }
        }

        //oriente l'unité dans le sens du déplacement
        if (posA != null && posA != transform.position)
            posB = posA;
        posA = transform.position;

        Vector3 dir = (posB - posA).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void GoTo(Cell target)
    {
        Cell start = currentCell;
        bool isValid = false, end = false; ;
        while (!isValid && !end)
        {
            for (int x = 0; x <= unit.moveRange; x++)
            {
                for (int y = 0; y <= unit.moveRange - x; y++)
                {
                    if (target.X <= start.X + x && target.X >= start.X - x &&
                        target.Y <= start.Y + y && target.Y >= start.Y - y)
                    {
                        isValid = true;
                    }
                    /*if (start.X + x < 100 && start.X - x >= 0 && start.Y + y < 20 && start.Y - y >= 0)
                    {
                        grid.grid[start.X + x, start.Y + y].SetMaterial(grid.chosen);
                        grid.grid[start.X - x, start.Y + y].SetMaterial(grid.chosen);
                        grid.grid[start.X + x, start.Y - y].SetMaterial(grid.chosen);
                        grid.grid[start.X - x, start.Y - y].SetMaterial(grid.chosen);
                    }*/
                }
            }
            end = true;
        }
        if(isValid)
        {
            start.onCell = null;
            start.SetBlocker(false);
            target.onCell = this.gameObject;
            if (pathList.Count > 0) start = pathList[pathList.Count - 1];
            pathList.AddRange(grid.PathFind(start, target));
            Animator anim;
            for (int i = 0; i < animated.Count; i++)
            {
                anim = animated[i].GetComponent<Animator>();
                anim.SetTrigger("WalkCycle");
            }
        }
    }
    public void FreeCell()
    {
        for (int x = 0; x <= unit.moveRange; x++)
        {
            for (int y = 0; y <= unit.moveRange - x; y++)
            {
                if (currentCell.X + x < 100 && currentCell.X - x >= 0 && currentCell.Y + y < 20 && currentCell.Y - y >= 0)
                {
                    if (!grid.grid[currentCell.X + x, currentCell.Y + y].isBlocked)
                        grid.grid[currentCell.X + x, currentCell.Y + y].SetIndic(true, acces);
                    if (!grid.grid[currentCell.X - x, currentCell.Y + y].isBlocked)
                        grid.grid[currentCell.X - x, currentCell.Y + y].SetIndic(true, acces);
                    if (!grid.grid[currentCell.X + x, currentCell.Y - y].isBlocked)
                        grid.grid[currentCell.X + x, currentCell.Y - y].SetIndic(true, acces);
                    if (!grid.grid[currentCell.X - x, currentCell.Y - y].isBlocked)
                        grid.grid[currentCell.X - x, currentCell.Y - y].SetIndic(true, acces);
                }
            }
        }
    }

    public void InRange()
    {
        for (int x = 0; x <= unit.attRange; x++)
        {
            for (int y = 0; y <= unit.attRange - x; y++)
            {
                if (currentCell.X + x < 100 && currentCell.X - x >= 0 &&
                    currentCell.Y + y < 20 && currentCell.Y - y >= 0)
                {
                    if (grid.grid[currentCell.X + x, currentCell.Y + y].onCell != null)
                        if (grid.grid[currentCell.X + x, currentCell.Y + y].onCell.GetComponent<EnemyUnit>() != null)
                            grid.grid[currentCell.X + x, currentCell.Y + y].SetIndic(true, enemy);
                    if (grid.grid[currentCell.X - x, currentCell.Y + y].onCell != null)
                        if (grid.grid[currentCell.X - x, currentCell.Y + y].onCell.GetComponent<EnemyUnit>() != null)
                            grid.grid[currentCell.X - x, currentCell.Y + y].SetIndic(true, enemy);
                    if (grid.grid[currentCell.X + x, currentCell.Y - y].onCell != null)
                        if (grid.grid[currentCell.X + x, currentCell.Y - y].onCell.GetComponent<EnemyUnit>() != null)
                            grid.grid[currentCell.X + x, currentCell.Y - y].SetIndic(true, enemy);
                    if (grid.grid[currentCell.X - x, currentCell.Y - y].onCell != null)
                        if (grid.grid[currentCell.X - x, currentCell.Y - y].onCell.GetComponent<EnemyUnit>() != null)
                            grid.grid[currentCell.X - x, currentCell.Y - y].SetIndic(true, enemy);
                }
            }
        }
    }
}
