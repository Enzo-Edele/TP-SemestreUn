using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<AllyUnit> playerUnits;  //numeroté les unit pour sup de la liste, idem pour enemy peut etre fait auto dans start
    public List<EnemyUnit> enemyUnits; //numéroté et quand un alien meur check si il en reste pour 
    //list pour missile

    public int leftBorder, rightBorder;

    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        menu,
        pause,
        inGame
    }
    public static GameState gameState { get; private set; }
    
    void Awake()
    {
        Instance = this;
        leftBorder = 0;
        rightBorder = 20;
    }
    public void ChangeGameState(GameState state)
    {
        gameState = state;
        switch(gameState)
        {
            case GameState.inGame:
                break;
            case GameState.menu:
                break;
            case GameState.pause:
                break;
        }
    }
    
    void StartPlayerTurn()
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].NewTurn();
        }
    }
    public void AITurn()
    {
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            enemyUnits[i].NewTurn();
        }
        StartPlayerTurn();
    }
    void LunchMissile()
    {

    }
}
