using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    //value for pathfinding
    public GridPathFinding grid;
    Cell currentCell;
    List<Cell> pathList;
    //value pour aligner dans la direction du mouvement
    Vector3 posA = new Vector3();
    Vector3 posB = new Vector3();

    public UnitScriptableObject alien;
    public int HP;
    int moveRange;
    int actionPoint;
    int actionPointMax;

    void Start()
    {
        Vector3 position = transform.position;
        currentCell = grid.grid[(int)position.x, (int)position.z];
        grid.grid[(int)position.x, (int)position.z].onCell = this.gameObject;
        grid.grid[(int)position.x, (int)position.z].SetBlocker(true);
        pathList = new List<Cell>();

        name = alien.Name;
        HP = alien.HP;
        moveRange = alien.moveRange;
        actionPoint = alien.actionPoint;
        actionPointMax = alien.actionPoint;
    }
    void Update()
    {
        //mouv par tour : mettre une limite au nombre de fois ou on bouge d'une case
        if (pathList.Count > 0 && actionPoint > 0) //while
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pathList[0].transform.position,
                Time.deltaTime * 5);
            if (transform.position == pathList[0].transform.position)
            {
                currentCell = pathList[0];
                pathList.RemoveAt(0);
                //actionPoint--;
            }
        }

        //oriente l'unité dans le sens du déplacement
        if (posA != null && posA != transform.position)
            posB = posA;
        posA = transform.position;

        Vector3 dir = (posB - posA).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    void Movement(Cell target)
    {
        //patrouille : mettre un liste de celle et quand on atteint un point on met le suivant en objectif (si bout de liste retour point 1)
        //mouv par tour : mettre une limite au nombre de fois ou on bouge d'une case
        Cell start = currentCell;
        bool isValid = false, end = false; ;
        while (!isValid && !end)
        {
            for (int x = 0; x <= moveRange; x++)
            {
                for (int y = 0; y <= moveRange - x; y++)
                {
                    if (target.X <= start.X + x && target.X >= start.X - x &&
                        target.Y <= start.Y + y && target.Y >= start.Y - y)
                    {
                        isValid = true;
                    }
                    /*if (grid.grid[start.X + x, start.Y + y] != null) 
                        grid.grid[start.X + x, start.Y + y].SetMaterial(grid.chosen);
                    if (grid.grid[start.X - x, start.Y + y] != null) 
                        grid.grid[start.X - x, start.Y + y].SetMaterial(grid.chosen);
                    if (grid.grid[start.X + x, start.Y - y] != null) 
                        grid.grid[start.X + x, start.Y - y].SetMaterial(grid.chosen);
                    if (grid.grid[start.X - x, start.Y - y] != null) 
                        grid.grid[start.X - x, start.Y - y].SetMaterial(grid.chosen); */
                }
            }
            end = true;
        }
        if (isValid)
        {
            start.onCell = null;
            target.onCell = this.gameObject;
            if (pathList.Count > 0) start = pathList[pathList.Count - 1];
            pathList.AddRange(grid.PathFind(start, target));
        }

        //essayer coroutine pour faire du unit by unit

        void NewTurn()
        {
            actionPoint = actionPointMax;
            if (GameManager.Instance.rightBorder > transform.position.x)
            {
                //activer ennemy
            }
        }
    }
}
