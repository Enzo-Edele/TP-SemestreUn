using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    //value for pathfinding
    public GridPathFinding grid;
    Cell currentCell;
    List<Cell> pathList;
    //assigne un chemin de patrouille au enemy
    [SerializeField] List<int> patrolPointX;
    [SerializeField] List<int> patrolPointY;
    List<Cell> patrolPoint;
    int currentPatrolPoint;
    //value pour aligner dans la direction du mouvement
    Vector3 posA = new Vector3();
    Vector3 posB = new Vector3();

    [SerializeField] UnitScriptableObject alien;
    int HP;
    int moveRange;
    int movePoint;
    int actionPoint;
    int actionPointMax;
    int attRange;
    int attDamage;

    [SerializeField] List<GameObject> animated;
    int orderInList;
    void Start()
    {
        Vector3 position = transform.position;
        currentCell = grid.grid[(int)position.x, (int)position.z];
        grid.grid[(int)position.x, (int)position.z].onCell = this.gameObject;
        grid.grid[(int)position.x, (int)position.z].SetBlocker(true);
        pathList = new List<Cell>();

        patrolPoint = new List<Cell>();
        for(int i = 0; i < patrolPointX.Count; i++)
        {
            patrolPoint.Add(grid.grid[patrolPointX[i], patrolPointY[i]]);
        }

        SetDestination(patrolPoint[currentPatrolPoint]);

        name = alien.Name;
        HP = alien.HP;
        moveRange = alien.moveRange;
        actionPoint = alien.actionPoint;
        actionPointMax = alien.actionPoint;
        movePoint = 0;
        attRange = alien.attRange;
        attDamage = alien.attDamage;

        orderInList = GameManager.Instance.enemyUnits.Count;
        GameManager.Instance.enemyUnits.Add(this);
    }
    void Update()
    {
        if (pathList.Count > 0 && movePoint > 0)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pathList[0].transform.position,
                Time.deltaTime * 1f);
            if (transform.position == pathList[0].transform.position)
            {
                currentCell.onCell = null;
                currentCell.SetBlocker(false);
                currentCell = pathList[0];
                currentCell.onCell = this.gameObject;
                pathList.RemoveAt(0);
                movePoint--;
                if(movePoint == 0)
                {
                    CheckActionLeft();
                    Animator anim;
                    for (int i = 0; i < animated.Count; i++)
                    {
                        anim = animated[i].GetComponent<Animator>();
                        anim.SetTrigger("WalkCycle");//pour ennemy faire un bool car bug quand plusieur dep succesif(surtout si pair)
                    }
                }
            }
        }

        //oriente l'unité dans le sens du déplacement
        if (posA != null && posA != transform.position)
            posB = posA;
        posA = transform.position;

        Vector3 dir = (posB - posA).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void NewTurn()
    {
        actionPoint = actionPointMax;
        CheckActionLeft();
        if (GameManager.Instance.rightBorder > transform.position.x)
        {
            //activer ennemy
        }
    }
    void CheckActionLeft()
    {
        if(actionPoint > 0)
        {
            for (int x = 0; x <= attRange; x++)
            {
                for (int y = 0; y <= attRange - x; y++)
                {
                    if (currentCell.X + x < 100 && currentCell.Y + y < 20)
                        if (grid.grid[currentCell.X + x, currentCell.Y + y].onCell != null)
                            if (grid.grid[currentCell.X + x, currentCell.Y + y].onCell.GetComponent<AllyUnit>() != null)
                            {
                                Debug.Log("alien shot");
                                Shoot(grid.grid[currentCell.X + x, currentCell.Y + y].onCell.GetComponent<AllyUnit>());
                                return;
                            }
                    if (currentCell.X - x >= 0 && currentCell.Y + y < 20)
                        if (grid.grid[currentCell.X - x, currentCell.Y + y].onCell != null)
                            if (grid.grid[currentCell.X - x, currentCell.Y + y].onCell.GetComponent<AllyUnit>() != null)
                            {
                                Debug.Log("alien shot");
                                Shoot(grid.grid[currentCell.X - x, currentCell.Y + y].onCell.GetComponent<AllyUnit>());
                                return;
                            }
                    if (currentCell.X + x < 100 && currentCell.Y - y >= 0)
                        if (grid.grid[currentCell.X + x, currentCell.Y - y].onCell != null)
                            if (grid.grid[currentCell.X + x, currentCell.Y - y].onCell.GetComponent<AllyUnit>() != null)
                            {
                                Debug.Log("alien shot");
                                Shoot(grid.grid[currentCell.X + x, currentCell.Y - y].onCell.GetComponent<AllyUnit>());
                                return;
                            }
                    if (currentCell.X - x >= 0 && currentCell.Y - y >= 0)
                        if (grid.grid[currentCell.X - x, currentCell.Y - y].onCell != null)
                            if (grid.grid[currentCell.X - x, currentCell.Y - y].onCell.GetComponent<AllyUnit>() != null)
                            {
                                Debug.Log("alien shot");
                                Shoot(grid.grid[currentCell.X - x, currentCell.Y - y].onCell.GetComponent<AllyUnit>());
                                return;
                            }
                }
            }
            Move();
        }
        else
        {
            GameManager.Instance.aliensTurnEnd--;
        }
    }
    void Shoot(AllyUnit target)
    {
        actionPoint--;
        target.ChangeHealth(attDamage);
        CheckActionLeft();
    }
    void SetDestination(Cell target)
    {
        Cell start = currentCell;
        if (pathList.Count > 0) start = pathList[pathList.Count - 1];
        pathList.AddRange(grid.PathFind(start, target));
        if (currentPatrolPoint < patrolPoint.Count - 1)
        {
            currentPatrolPoint++;
        }
        else
        {
            currentPatrolPoint = 0;
        }
        //essayer coroutine pour faire du unit by unit
    }
    void Move()
    {
        actionPoint--;
        movePoint = moveRange;
        if(pathList.Count == 0)
        {
            SetDestination(patrolPoint[currentPatrolPoint]);
        }
        Animator anim;
        for (int i = 0; i < animated.Count; i++)
        {
            anim = animated[i].GetComponent<Animator>();
            anim.SetTrigger("WalkCycle");
        }
    }

    public void ChangeHealth(int damage)
    {
        HP -= damage;
        if (HP < 1)
        {
            GameManager.Instance.enemyUnits.RemoveAt(orderInList);
            UIManager.Instance.UnSelect();
        }
        DisplayInfo();
    }
    public void DisplayInfo()
    {
        UIManager.Instance.Select(name, HP, alien.HP, actionPoint, actionPointMax, attRange, attDamage, moveRange);
    }
}
