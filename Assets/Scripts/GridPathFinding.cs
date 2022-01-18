using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFinding : MonoBehaviour
{
    public GameObject cellPrefab;
    public Vector2Int sizeGrid;
    public Cell[,] grid;

    public Material basic, visited, chosen; //a virer pour la fin

    void Start()
    {
        //génére la grille
        grid = new Cell[sizeGrid.x, sizeGrid.y];
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                GameObject go = Instantiate(cellPrefab);
                go.name = "Cell [" + x + ";" + y + "]";
                go.transform.position = new Vector3(x, -0.6f, y);
                grid[x, y] = go.GetComponent<Cell>();
            }
        }
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                Cell c = grid[x, y];
                c.neighbors = new List<Cell>();
                if (x > 0) c.neighbors.Add(grid[x - 1, y]);
                if (y > 0) c.neighbors.Add(grid[x, y - 1]);
                if (x < sizeGrid.x - 1) c.neighbors.Add(grid[x + 1, y]);
                if (y < sizeGrid.y - 1) c.neighbors.Add(grid[x, y + 1]);
            }
        }
    }
    //fonction pour reset la recherche de chemin
    void ResetGrid()
    {
        for (int y = 0; y < sizeGrid.y; y++)
        {
            for (int x = 0; x < sizeGrid.x; x++)
            {
                Cell c = grid[x, y];
                c.visited = false;
                //c.SetMaterial(basic); //a virer
                c.node = null;
                c.parent = null;
            }
        }
    }
    //fonction du pathfinding
    public List<Cell> PathFind(Cell start, Cell target)
    {
        ResetGrid();
        PriorityHeap<Cell> frontier = new PriorityHeap<Cell>(); //Frontier = frontier des trucs a parcourir
        start.node = frontier.Insert(start, 0);
        while (!frontier.IsEmpty())
        {
            Node<Cell> current = frontier.PopMin();
            Cell cell = current.content;
            cell.visited = true;
            //cell.SetMaterial(visited); //a virer
            if (cell == target) break;

            foreach (Cell neigh in cell.neighbors)
            {
                if (neigh.visited) continue;
                if (neigh.isBlocked) continue;

                if (neigh.node == null)
                {
                    neigh.node = frontier.Insert(neigh, current.priority + 1);
                    neigh.parent = cell;
                }
                else if (neigh.node.priority > current.priority + 1)  
                {
                    frontier.ChangePriority(neigh.node, current.priority + 1);
                    neigh.parent = cell;
                }
            }
        }

        List<Cell> res = new List<Cell>();
        Cell currentCell = target;
        while (currentCell.parent != null)
        {
            res.Add(currentCell);
            //currentCell.SetMaterial(chosen); //a virer
            currentCell = currentCell.parent;
            if (currentCell.parent == null)
            {
                //currentCell.SetMaterial(chosen); //a virer
            }
        }
        res.Reverse();
        return res; 
    }
}
